using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ProducerApi.Kafka;

public class SendMessageDto
{
    [FromRoute]
    public string Topic { get; set; }

    [Required]
    [FromBody]
    public MessageInfo Info { get; set; }
    public class MessageInfo
    {
        public string? Key { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
