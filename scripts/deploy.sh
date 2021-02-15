#!/usr/bin/env bash

gcloud functions deploy egginc-api-dotnet --entry-point HttpFunction.Function --runtime dotnet3 --trigger-http --env-vars-file .env.yaml --allow-unauthenticated