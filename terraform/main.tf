provider "aws" {
  region = var.aws_region
}

# 1. Tạo VPC (Virtual Private Cloud)
resource "aws_vpc" "ecommerce_vpc" {
  cidr_block           = "10.0.0.0/16"
  enable_dns_support   = true
  enable_dns_hostnames = true
  tags = {
    Name = "ecommerce-vpc"
  }
}

# 2. Tạo Internet Gateway để có thể truy cập Internet
resource "aws_internet_gateway" "gw" {
  vpc_id = aws_vpc.ecommerce_vpc.id
  tags = {
    Name = "ecommerce-igw"
  }
}

# 3. Tạo Route Table
resource "aws_route_table" "public_rt" {
  vpc_id = aws_vpc.ecommerce_vpc.id
  route {
    cidr_block = "0.0.0.0/0"
    gateway_id = aws_internet_gateway.gw.id
  }
  tags = {
    Name = "ecommerce-public-rt"
  }
}

# 4. Tạo Subnet Public
resource "aws_subnet" "public_subnet" {
  vpc_id                  = aws_vpc.ecommerce_vpc.id
  cidr_block              = "10.0.1.0/24"
  map_public_ip_on_launch = true
  availability_zone       = "${var.aws_region}a"
  tags = {
    Name = "ecommerce-public-subnet"
  }
}

# Gắn Route table vào Subnet
resource "aws_route_table_association" "public_assoc" {
  subnet_id      = aws_subnet.public_subnet.id
  route_table_id = aws_route_table.public_rt.id
}

# 5. Security Group
resource "aws_security_group" "web_sg" {
  name        = "ecommerce-web-sg"
  description = "Allow HTTP, HTTPS and SSH"
  vpc_id      = aws_vpc.ecommerce_vpc.id

  # Allow HTTP
  ingress {
    from_port   = 80
    to_port     = 80
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }

  # Allow HTTPS
  ingress {
    from_port   = 443
    to_port     = 443
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }

  # Allow SSH
  ingress {
    from_port   = 22
    to_port     = 22
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }
  
  # Giám sát (Prometheus & Grafana)
  ingress {
    from_port   = 9090
    to_port     = 9090
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }
  
  ingress {
    from_port   = 3000
    to_port     = 3000
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }

  egress {
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = ["0.0.0.0/0"]
  }
}

# 6. EC2 Instance để chạy Docker Compose
resource "aws_instance" "app_server" {
  ami             = "ami-0c7217cdde317cfec" # Ubuntu 22.04 LTS (us-east-1), hãy đổi tuỳ theo region!
  instance_type   = "t2.micro" # Free tier
  subnet_id       = aws_subnet.public_subnet.id
  security_groups = [aws_security_group.web_sg.id]
  key_name        = var.ssh_key_name

  tags = {
    Name = "ecommerce-app-server"
  }

  # Script cài đặt Docker khi khởi động (User Data)
  user_data = <<-EOF
              #!/bin/bash
              apt-get update -y
              apt-get install ca-certificates curl gnupg lsb-release -y
              mkdir -p /etc/apt/keyrings
              curl -fsSL https://download.docker.com/linux/ubuntu/gpg | gpg --dearmor -o /etc/apt/keyrings/docker.gpg
              echo "deb [arch=$(dpkg --print-architecture) signed-by=/etc/apt/keyrings/docker.gpg] https://download.docker.com/linux/ubuntu $(lsb_release -cs) stable" | tee /etc/apt/sources.list.d/docker.list > /dev/null
              apt-get update -y
              apt-get install docker-ce docker-ce-cli containerd.io docker-compose-plugin -y
              usermod -aG docker ubuntu
              EOF
}
