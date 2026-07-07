variable "aws_region" {
  description = "Region AWS để triển khai hạ tầng"
  type        = string
  default     = "us-east-1"
}

variable "ssh_key_name" {
  description = "Tên của AWS Key Pair đã được tạo trước để truy cập SSH vào EC2"
  type        = string
  default     = "ecommerce-key" 
}
