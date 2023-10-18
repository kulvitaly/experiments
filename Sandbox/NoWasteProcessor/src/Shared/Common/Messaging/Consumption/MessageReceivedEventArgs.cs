using Domain.SharedKernel;

namespace Application.Messaging.Consumption;

public class MessageReceivedEventArgs : EventArgs
{
    public MessageReceivedEventArgs(Message message) => Message = message;

    public Message Message { get; }
}
