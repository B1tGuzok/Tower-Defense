using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    private bool isMuted = false;
    public Image buttonImage; // Ссылка на компонент Image кнопки
    public Sprite noteOnSprite; // Спрайт для включенного звука
    public Sprite noteOffSprite; // Спрайт для выключенного звука

    void Start()
    {
        // Проверка сохраненного состояния звука
        isMuted = PlayerPrefs.GetInt("Muted", 0) == 1;
        AudioListener.volume = isMuted ? 0 : 1; // Установка громкости
        UpdateButtonSprite(); // Обновление спрайта кнопки
    }

    public void ToggleSound()
    {
        isMuted = !isMuted; // Переключение состояния
        AudioListener.volume = isMuted ? 0 : 1; // Установка громкости

        // Сохранение состояния звука
        PlayerPrefs.SetInt("Muted", isMuted ? 1 : 0);
        PlayerPrefs.Save();

        UpdateButtonSprite(); // Обновление спрайта кнопки
    }

    private void UpdateButtonSprite()
    {
        if (isMuted)
        {
            buttonImage.sprite = noteOffSprite; // Устанавливаем спрайт для выключенного звука
        }
        else
        {
            buttonImage.sprite = noteOnSprite; // Устанавливаем спрайт для включенного звука
        }
    }
}
