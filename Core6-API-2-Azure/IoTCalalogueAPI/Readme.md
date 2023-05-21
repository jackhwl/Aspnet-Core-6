## Section 2: Azure API Management Overviwe
* Demo: Importing Our API to Azure API Management
## Section 3: Deploying APIs to Azure API Management
* Demo: Provision API Management in Azure Portal
* Demo: Provision API Management in Azure CLI
* az login
* az account set --subscription a6dfa2b8-35fa-462c-8435-9b5da92438ae

* az apim create --name WenlinAPI-APIM-CLI -g WenlinAPI-RG -l eastus --publisher-email wenlin.huang@aderant.com --publisher-name aderant
* az apim show --name Wenlin-APIM --resource-group WenlinAPI-RG --query "{Name:name, Sku:sku.name}"
* az apim api create --service-name Wenlin-APIM --resource-group WenlinAPI-RG --api-id MyApi --path '/myapi' --display-name 'My API'
* Demo: Prevent Bypassing API Management
```
        <set-header name="SecurityToken" exists-action="override">
            <value>pass1234</value>
        </set-header>
```
* Demo: Save API Management Cofiguration in Git Repository
* Demo: API Management Products and Subscriptions