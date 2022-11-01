module "budgets" {
  source = "./budgets"
}

module "domain" {
  source              = "./domains"
  endpoint            = "www.awsconcepts.com"
  domain_name         = "awsconcepts.com"
  website_bucket_name = "www.awsconcepts.com"
}