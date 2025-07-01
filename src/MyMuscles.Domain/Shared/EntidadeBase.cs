namespace MyMuscles.Domain.Shared;

public abstract class EntidadeBase : Notificavel
{
    protected EntidadeBase()
    {
        Id = Guid.NewGuid();
        CriadoEm = DateTime.UtcNow;
        AtualizadoEm = DateTime.UtcNow;
    }

    public Guid Id { get; private set; }
    public DateTime CriadoEm { get; private set; }
    public DateTime AtualizadoEm { get; protected set; }
}
