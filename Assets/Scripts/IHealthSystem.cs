using System;

public interface IHealthSystem
{
    event Action<int, int> OnHPChanged;
    int GetCurrentHealth();
    void ChageHP(int value);
}
