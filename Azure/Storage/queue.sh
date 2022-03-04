# create a queue
az storage queue create --name mysamplequeue

# delete a queue
az storage queue delete --name mysamplequeue

# view messages in a queue (without affecting visivility)
az storage message peek --queue-name mysamplequeue --num-messages 32

# view messages in a queue
az storage message get --queue-name mysamplequeue

az storage message delete --queue-name mysamplequeue --id 8a264222-5674-4983-a965-02424349c857 --pop-receipt AgAAAAMAAAAAAAAABmK8eEnc1wE=

# delete all messages in a queue
az storage message clear --queue-name mysamplequeue