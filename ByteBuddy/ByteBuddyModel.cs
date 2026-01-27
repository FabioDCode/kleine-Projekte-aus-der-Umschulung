public class ByteBBlock
{
    public Guid Id { get; set; } = Guid.NewGuid();

    // z.B. Move, Turn, Repeat
    public ByteBBlock Type { get; set; }

    // Parameter wie Schritte, Grad, Wiederholungen
    public Dictionary<string, object> Parameters { get; set; } 
        = new Dictionary<string, object>();

    // Nächster Block in der Kette
    public ByteBBlock Next { get; set; }

    // Für Blöcke wie "Repeat", "If"
    public List<ByteBBlock> Children { get; set; } 
        = new List<ByteBBlock>();
}