resource "aws_budgets_budget" "MonthlyBudget" {
  # ...
  budget_type       = "COST"
  limit_amount      = "200"
  limit_unit        = "USD"
  time_unit         = "MONTHLY"
  time_period_start = "2022-10-01_00:01"
}