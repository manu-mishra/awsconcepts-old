resource "aws_route53_zone" "main" {
  name = "awsconcepts.com"
}

resource "aws_route53_record" "websiteurl" {
  zone_id = aws_route53_zone.main.zone_id
  name    = var.endpoint
  type    = "A"
  alias {
    name                   = aws_cloudfront_distribution.cf.domain_name
    zone_id                = aws_cloudfront_distribution.cf.hosted_zone_id
    evaluate_target_health = true
  }
}

resource "aws_route53_record" "www_url" {
  zone_id = aws_route53_zone.main.zone_id
  name    = "www.${var.endpoint}"
  type    = "CNAME"
  ttl = "300"
  records = [var.endpoint]
}

resource "aws_route53_record" "certvalidation" {
  for_each = {
    for d in aws_acm_certificate.cert.domain_validation_options : d.domain_name => {
      name   = d.resource_record_name
      record = d.resource_record_value
      type   = d.resource_record_type
    }
  }
  allow_overwrite = true
  name            = each.value.name
  records         = [each.value.record]
  ttl             = 30
  type            = each.value.type
  zone_id         = aws_route53_zone.main.zone_id
}

resource "aws_acm_certificate_validation" "certvalidation" {
  certificate_arn         = aws_acm_certificate.cert.arn
  validation_record_fqdns = [for r in aws_route53_record.certvalidation : r.fqdn]
}