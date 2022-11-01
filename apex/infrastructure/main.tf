module "budgets" {
  source = "./budgets"
}

module "traffic_control" {
  source              = "./traffic_control"
  endpoint            = "www.awsconcepts.com"
  domain_name         = "awsconcepts.com"
  website_bucket_name = "www.awsconcepts.com"
}