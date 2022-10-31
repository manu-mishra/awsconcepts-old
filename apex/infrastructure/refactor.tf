# moved {
#   from  = aws_budgets_budget.MonthlyBudget
#   to = module.budgets.aws_budgets_budget.MonthlyBudget
# }

# moved {
#   from  = aws_route53_zone.main
#   to = module.domain.aws_route53_zone.main
# }

# moved {
#   from  = aws_route53_zone.dev
#   to = module.domain.aws_route53_zone.dev
# }

# moved {
#   from  = aws_route53_record.dev-ns 
#   to = module.domain.aws_route53_record.dev-ns 
# }