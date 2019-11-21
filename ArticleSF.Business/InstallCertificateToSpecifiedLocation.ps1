#$Configobj = .\SetupApplications.ps1 -TenantId 'bb466cbb-621c-493b-8374-6c8f26dc6a18' -ClusterName 'article' -WebApplicationReplyUrl 'https://article.eastus.cloudapp.azure.com:19080/Explorer' -AddResourceAccess

Import-PfxCertificate -Exportable -CertStoreLocation Cert:\CurrentUser\My `
        -FilePath F:\Developer\mycertificates\articlekv-articlecert-20191106.pfx 
#        -Password (ConvertTo-SecureString -String test -AsPlainText -Force)

Import-PfxCertificate -Exportable -CertStoreLocation Cert:\CurrentUser\My `
        -FilePath F:\Developer\mycertificates\articlekv-articlesfcert-20191107.pfx 
#        -Password (ConvertTo-SecureString -String test -AsPlainText -Force)


Import-PfxCertificate -Exportable -CertStoreLocation Cert:\CurrentUser\TrustedPeople `
-FilePath F:\Developer\mycertificates\articlekv-articlecert-20191106.pfx 
#-Password (ConvertTo-SecureString -String test -AsPlainText -Force)

Import-PfxCertificate -Exportable -CertStoreLocation Cert:\CurrentUser\TrustedPeople `
-FilePath F:\Developer\mycertificates\articlekv-articlesfcert-20191107.pfx 
#-Password (ConvertTo-SecureString -String test -AsPlainText -Force)

Import-PfxCertificate -Exportable -CertStoreLocation Cert:\CurrentUser\TrustedPeople `
-FilePath F:\Developer\mycertificates\articlesf20191107125316-articlesf20191107125336-20191107.pfx 
#-Password (ConvertTo-SecureString -String test -AsPlainText -Force)

Import-PfxCertificate -Exportable -CertStoreLocation Cert:\CurrentUser\My `
-FilePath F:\Developer\mycertificates\articlesf20191107125316-articlesf20191107125336-20191107.pfx 
#        -Password (ConvertTo-SecureString -String test -AsPlainText -Force)