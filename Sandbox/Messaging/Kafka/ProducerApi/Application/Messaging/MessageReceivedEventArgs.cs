using Domain.SharedKernel;

namespace Application.Messaging;

public class MessageReceivedEventArgs : EventArgs
{
    public MessageReceivedEventArgs(Message message) => Message = message;

    public Message Message { get; }
}
