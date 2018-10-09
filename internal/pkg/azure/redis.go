package azure

import (
	"github.com/Azure/azure-sdk-for-go/services/redis/mgmt/2018-03-01/redis"
)

func visitRedisCache(info ResourceInfo) {
	client := redis.NewClient(info.SubscriptionID)
	client.Authorizer = GetAuthorizer()
}