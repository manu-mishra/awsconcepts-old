# moved {
#   from  = module.domain.data.aws_iam_policy_document.s3_cf_policy
#   to = module.traffic_control.data.aws_iam_policy_document.s3_cf_policy
# }

# moved {
#   from  = module.domain.aws_acm_certificate.cert
#   to = module.traffic_control.aws_acm_certificate.cert
# }

# moved {
#   from  = module.domain.aws_acm_certificate_validation.certvalidation
#   to = module.traffic_control.aws_acm_certificate_validation.certvalidation
# }

# moved {
#   from  = module.domain.aws_cloudfront_distribution.cf
#   to = module.traffic_control.aws_cloudfront_distribution.cf
# }

# moved {
#   from  = module.domain.aws_cloudfront_origin_access_identity.oai
#   to = module.traffic_control.aws_cloudfront_origin_access_identity.oai
# }

# moved {
#   from  = module.domain.aws_route53_record.certvalidation["*.awsconcepts.com"]
#   to = module.traffic_control.aws_route53_record.certvalidation["*.awsconcepts.com"]
# }

# moved {
#   from  = module.domain.aws_route53_record.certvalidation["awsconcepts.com"]
#   to = module.traffic_control.aws_route53_record.certvalidation["awsconcepts.com"]
# }

# moved {
#   from  = module.domain.aws_route53_record.websiteurl
#   to = module.traffic_control.aws_route53_record.websiteurl
# }

# moved {
#   from  = module.domain.aws_route53_zone.main
#   to = module.traffic_control.aws_route53_zone.main
# }

# moved {
#   from  = module.domain.aws_s3_bucket.website
#   to = module.traffic_control.aws_s3_bucket.website
# }

# moved {
#   from  = module.domain.aws_s3_bucket_acl.website-bucket-acl
#   to = module.traffic_control.aws_s3_bucket_acl.website-bucket-acl
# }
# moved {
#   from  = module.domain.aws_s3_bucket_policy.s3_cf_policy
#   to = module.traffic_control.aws_s3_bucket_policy.s3_cf_policy
# }
# moved {
#   from  = module.domain.aws_s3_bucket_public_access_block.website_s3block
#   to = module.traffic_control.aws_s3_bucket_public_access_block.website_s3block
# }
# moved {
#   from  = module.domain.aws_s3_bucket_server_side_encryption_configuration.encryption_at_rest
#   to = module.traffic_control.aws_s3_bucket_server_side_encryption_configuration.encryption_at_rest
# }
