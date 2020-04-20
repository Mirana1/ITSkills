namespace ITSkills.Services.Data
{
    public interface ILectionsService
    {
        T GetById<T>(int id);
    }
}
