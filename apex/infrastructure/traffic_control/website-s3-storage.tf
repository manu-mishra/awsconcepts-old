resource "aws_s3_bucket" "website" {
  bucket        = var.website_bucket_name
  force_destroy = true
  tags          = local.tags
}
resource "aws_s3_bucket_server_side_encryption_configuration" "encryption_at_rest" {
  bucket = aws_s3_bucket.website.id
  rule {
    apply_server_side_encryption_by_default {
      sse_algorithm = "AES256"
    }
  }
}
resource "aws_s3_bucket_public_access_block" "website_s3block" {
  bucket                  = aws_s3_bucket.website.id
  block_public_acls       = true
  block_public_policy     = true
  ignore_public_acls      = true
  restrict_public_buckets = true
}
resource "aws_s3_bucket_acl" "website-bucket-acl" {
  bucket = aws_s3_bucket.website.id
  acl    = "private"
}
resource "aws_s3_bucket_policy" "s3_cf_policy" {
  bucket = aws_s3_bucket.website.id
  policy = data.aws_iam_policy_document.s3_cf_policy.json
}