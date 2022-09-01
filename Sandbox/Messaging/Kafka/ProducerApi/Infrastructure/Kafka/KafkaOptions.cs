using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Kafka;

public class KafkaOptions
{
    public const string Kafka = "Kafka";

    public string BootstrapServer { get; set; }
}
