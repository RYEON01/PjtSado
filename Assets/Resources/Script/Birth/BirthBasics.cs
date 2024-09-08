using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Globalization;

public class BirthBasics : MonoBehaviour
{
    public ElementAssigner elementAssigner;
    
    public TMP_Dropdown yearDropdown;
    public TMP_Dropdown monthDropdown;
    public TMP_Dropdown dateDropdown;
    public TMP_Dropdown timeDropdown;
    public Toggle calendarToggle;
    public Button confirmButton;

    private int selectedYear;
    private int selectedMonth;
    private int selectedDate;
    private string selectedTime;
    
    private int lunarYear;
    private int lunarMonth;
    private int lunarDay;
    
    void Start()
    {
        elementAssigner = GetComponent<ElementAssigner>();
        if (elementAssigner == null)
        {
            Debug.LogError("ElementAssigner is null");
            return;
        }
        PopulateDropdowns();
        calendarToggle.onValueChanged.AddListener(delegate {ToggleValueChanged(calendarToggle);});
        confirmButton.onClick.AddListener(ConfirmSelections);
    }

    void PopulateDropdowns()
    {
        if (calendarToggle.isOn)
        {
            PopulateLunarCalendar();
        }
        else
        {
            PopulateSolarCalendar();
        }
    }

    void PopulateSolarCalendar()
    {
        // Populate the dropdowns for the solar calendar
        PopulateYearDropdown();
        PopulateMonthDropdown();
        PopulateDateDropdown();
        PopulateTimeDropdown();
    }

    void PopulateLunarCalendar()
    {
        PopulateLunarYearDropdown();
        PopulateLunarMonthDropdown();
        PopulateLunarDateDropdown();
        PopulateTimeDropdown();
    }

    void ToggleValueChanged(Toggle change)
    {
        // Clear the existing options in the dropdowns
        yearDropdown.ClearOptions();
        monthDropdown.ClearOptions();
        dateDropdown.ClearOptions();
        timeDropdown.ClearOptions();

        // Repopulate the dropdowns based on the selected calendar
        PopulateDropdowns();
    }
    
    void ConfirmSelections()
    {
        
        selectedYear = int.Parse(yearDropdown.options[yearDropdown.value].text);
        selectedMonth = int.Parse(monthDropdown.options[monthDropdown.value].text);
        selectedDate = int.Parse(dateDropdown.options[dateDropdown.value].text);
        selectedTime = timeDropdown.options[timeDropdown.value].text;
        
        if (!calendarToggle.isOn)
        {
            ConvertSolarToLunar(selectedYear, selectedMonth, selectedDate);
            // Use the lunar dates to assign the stats
            elementAssigner.Assign10and12(lunarYear, lunarMonth, lunarDay, selectedTime);
        }
        else
        {
            // Use the selected dates to assign the stats
            elementAssigner.Assign10and12(selectedYear, selectedMonth, selectedDate, selectedTime);
        }
        
        BattleCharacter assignedCharacter = elementAssigner.Assign10and12(selectedYear, selectedMonth, selectedDate, selectedTime);
        
        BCPlayer playerInstance = assignedCharacter as BCPlayer;
        if (playerInstance != null)
        {
            GameManager.Instance.BattleSystem.Player = playerInstance;
            playerInstance.SaveStats();
        }
        else
        {
            Debug.LogError("Assigned character is not a BCPlayer");
        }

        GameObject enemyObject = new GameObject("Enemy");
        BCCheongi enemyInstance = enemyObject.AddComponent<BCCheongi>();
        GameManager.Instance.BattleSystem.Enemy = enemyInstance;
    }

    void PopulateYearDropdown()
    {
        List<string> options = new List<string>();

        for (int year = 1910; year <= System.DateTime.Now.Year; year++)
        {
            options.Add(year.ToString());
        }

        yearDropdown.AddOptions(options);
    }

    void PopulateMonthDropdown()
    {
        List<string> options = new List<string>();

        for (int month = 1; month <= 12; month++)
        {
            options.Add(month.ToString());
        }

        monthDropdown.AddOptions(options);
    }

    void PopulateDateDropdown()
    {
        List<string> options = new List<string>();

        for (int date = 1; date <= 31; date++)
        {
            options.Add(date.ToString());
        }

        dateDropdown.AddOptions(options);
    }
    
    void PopulateLunarYearDropdown()
    {
        List<string> options = new List<string>();

        // The lunar calendar year range might be different from the solar calendar
        for (int year = 1910; year <= System.DateTime.Now.Year + 3; year++)
        {
            options.Add(year.ToString());
        }

        yearDropdown.AddOptions(options);
    }

    void PopulateLunarMonthDropdown()
    {
        List<string> options = new List<string>();

        // The lunar calendar typically has 12 months
        for (int month = 1; month <= 12; month++)
        {
            options.Add(month.ToString());
        }

        monthDropdown.AddOptions(options);
    }

    void PopulateLunarDateDropdown()
    {
        List<string> options = new List<string>();

        // Each month in the lunar calendar has 29 or 30 days
        for (int date = 1; date <= 30; date++)
        {
            options.Add(date.ToString());
        }

        dateDropdown.AddOptions(options);
    }

    void PopulateTimeDropdown()
    {
        List<string> options = new List<string>();

        for (int hour = 0; hour < 24; hour++)
        {
            for (int minute = 0; minute < 60; minute += 30)
            {
                options.Add(hour.ToString("D2") + ":" + minute.ToString("D2"));
            }
        }

        timeDropdown.AddOptions(options);
    }
    
    void ConvertSolarToLunar(int year, int month, int day)
    {
        if (year < 1 || year > 9999)
        {
            Debug.LogError("Invalid year: " + year);
            return;
        }

        if (month < 1 || month > 12)
        {
            Debug.LogError("Invalid month: " + month);
            return;
        }

        if (day < 1 || day > 31)
        {
            Debug.LogError("Invalid day: " + day);
            return;
        }

        ChineseLunisolarCalendar calendar = new ChineseLunisolarCalendar();
        try
        {
            lunarYear = calendar.GetYear(new DateTime(year, month, day));
            lunarMonth = calendar.GetMonth(new DateTime(year, month, day));
            lunarDay = calendar.GetDayOfMonth(new DateTime(year, month, day));
        }
        catch (ArgumentOutOfRangeException e)
        {
            Debug.LogError("The date is out of range: " + e.Message);
        }
    }
}