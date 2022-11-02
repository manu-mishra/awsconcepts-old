module "budgets" {
  source = "./budgets"
}

module "traffic_control" {
  source              = "./traffic_control"
  endpoint            = "awsconcepts.com"
  domain_name         = "awsconcepts.com"
  website_bucket_name = "awsconcepts.com"
}