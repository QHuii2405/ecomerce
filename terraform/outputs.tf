output "server_public_ip" {
  description = "Địa chỉ IP Public của EC2 Instance. Sử dụng IP này để trỏ Domain hoặc truy cập ứng dụng."
  value       = aws_instance.app_server.public_ip
}

output "server_public_dns" {
  description = "Địa chỉ DNS Public của EC2 Instance."
  value       = aws_instance.app_server.public_dns
}
