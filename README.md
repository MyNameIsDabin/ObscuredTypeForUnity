# ObscuredTypeForUnity

핵 방지 타입 변수

```
public class Example : MonoBehaviour
{
    public Obscured<int> Score = new Obscured<int>(20);
    public Obscured<string> Name = new Obscured<string>("ABC");

    void Start()
    {
        Debug.Log($"Score : {Score.Value}"); // 20
        Score.Value = 32;
        Debug.Log($"Score : {Score.Value}"); // 32
        Score.Hack_SetValue(52);
        Debug.Log($"Score : {Score.Value}"); // 32

        Debug.Log($"Name : {Name.Value}"); // ABC
        Name.Value = "CDE";
        Debug.Log($"Name : {Name.Value}"); // CDE
        Name.Hack_SetValue("FGH");
        Debug.Log($"Name : {Name.Value}"); // CDE
    }
}
```
