resource "aws_s3_bucket" "awsconcepts-terraform-deployment-bucket" {
  bucket = "awsconcepts-terraform-deployments"
  tags = {
    Name        = "deployment bucket"
    Environment = "Apex"
  }
}

resource "aws_s3_bucket_acl" "awsconcepts-terraform-deployment-bucket-acl" {
  bucket = aws_s3_bucket.awsconcepts-terraform-deployment-bucket.id
  acl    = "private"
}
resource "aws_s3_bucket_versioning" "awsconcepts-terraform-deployment-bucket-versioning" {
  bucket = aws_s3_bucket.awsconcepts-terraform-deployment-bucket.id
  versioning_configuration {
    status = "Enabled"
  }
}
resource "aws_s3_bucket_public_access_block" "awsconcepts-terraform-deployment-bucket-block_public_policy" {
  bucket = aws_s3_bucket.awsconcepts-terraform-deployment-bucket.id
  block_public_acls       = true
  block_public_policy     = true
  ignore_public_acls      = true
  restrict_public_buckets = true
}