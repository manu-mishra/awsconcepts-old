terraform {
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "~> 4.16"
    }
  }

  backend "s3" {
    bucket = "awsconcepts-terraform-deployments"
    key = "awsconcepts/apex/terraform.tfstate"
    region  = "us-east-1"
    profile = "awsconcepts_deployment"
  }
  required_version = ">= 1.2.0"
}

provider "aws" {
  region  = "us-east-1"
  profile = "awsconcepts_deployment"
}

