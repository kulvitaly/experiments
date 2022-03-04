# create queue
az servicebus queue create --namespace-name pluralsight --name testqueue --resource-group pluralsight

# delete queue
az servicebus delete --namespace-name pluralsight --name testqueue

# create a topic
az servicebus topic create --namespace-name pluralsight --name testtopic --resource-group pluralsight 

# delete a topic 
az servicebus topic delete --namespace-name pluralsight --name testtopic

# create a subscription
az servicebus topic subscription create --namespace-name pluralsight --name testsub --topic-name testtopic
