using Newtonsoft.Json;
using System.Text;

namespace RazorPageWebApp.Extensions;

public static class SessionExtensions
{
    public static void SetEntity(this ISession session, string key, object? value)
    {
        if (value == null) throw new JsonException(nameof(value) + " is null");
        session.SetString(key, JsonConvert.SerializeObject(value));
    }
    public static T? GetEntity<T>(this ISession session)
    {
        return session.GetEntity<T>(nameof(T));
    }

    public static T? GetEntity<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        return value == null ? default : JsonConvert.DeserializeObject<T>(value);
    }
}
