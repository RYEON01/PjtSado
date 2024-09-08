using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementAssigner : MonoBehaviour
{
    public enum FiveElements
    {
        Wood,
        Fire,
        Earth,
        Metal,
        Water
    }
    
    public FiveElements AssignElement(int value, bool is10)
    {
        FiveElements element = FiveElements.Metal;

        if (is10)
        {
            switch (value)
            {
                case 0:
                case 1:
                    element = FiveElements.Metal;
                    break;
                case 2:
                case 3:
                    element = FiveElements.Water;
                    break;
                case 4:
                case 5:
                    element = FiveElements.Wood;
                    break;
                case 6:
                case 7:
                    element = FiveElements.Fire;
                    break;
                case 8:
                case 9:
                    element = FiveElements.Earth;
                    break;
            }
        }
        else
        {
            switch (value)
            {
                case 0:
                case 1:
                case 2:
                    element = FiveElements.Metal;
                    break;
                case 3:
                case 4:
                case 5:
                    element = FiveElements.Water;
                    break;
                case 6:
                case 7:
                case 8:
                    element = FiveElements.Wood;
                    break;
                case 9:
                case 10:
                case 11:
                    element = FiveElements.Fire;
                    break;
            }
        }

        return element;
    }
    
    public BCPlayer Assign10and12(int selectedYear, int selectedMonth, int selectedDate, string selectedTime)
    {
        int year10 = (selectedYear % 10);
        int year12 = selectedYear % 12;
        int month10 = CalculateMonth10(selectedMonth, year10);
        int month12 = (selectedMonth + 5) % 12;
        int date10 = CalculateDate10(selectedDate, year10);
        int date12 = (selectedDate + 7) % 12;
        int time10 = CalculateTime10(selectedTime, year10);
        int time12 = CalculateTime12(selectedTime);

        FiveElements yearA = AssignElement(year10, true);
        FiveElements yearB = AssignElement(year12, false);
        FiveElements monthA = AssignElement(month10, true);
        FiveElements monthB = AssignElement(month12, false);
        FiveElements dateA = AssignElement(date10, true);
        FiveElements dateB = AssignElement(date12, false);
        FiveElements timeA = AssignElement(time10, true);
        FiveElements timeB = AssignElement(time12, false);
        
        BCPlayer character = BCPlayer.PlayerInstance;
        character.WoodElement = CountElement(FiveElements.Wood, yearA, yearB, monthA, monthB, dateA, dateB, timeA, timeB);
        character.FireElement = CountElement(FiveElements.Fire, yearA, yearB, monthA, monthB, dateA, dateB, timeA, timeB);
        character.EarthElement = CountElement(FiveElements.Earth, yearA, yearB, monthA, monthB, dateA, dateB, timeA, timeB);
        character.MetalElement = CountElement(FiveElements.Metal, yearA, yearB, monthA, monthB, dateA, dateB, timeA, timeB);
        character.WaterElement = CountElement(FiveElements.Water, yearA, yearB, monthA, monthB, dateA, dateB, timeA, timeB);

        character.WaterStat = (character.WaterElement * 10) + 10;
        character.FireStat = (character.FireElement * 10) + 10;
        character.EarthStat = (character.EarthElement * 10) + 10;
        character.WoodStat = (character.WoodElement * 10) + 10;
        character.MetalStat = (character.MetalElement * 10) + 10;

        return character;
    }
    
    public int CountElement(FiveElements element, FiveElements yearA, FiveElements yearB, FiveElements monthA, FiveElements monthB, FiveElements dateA, FiveElements dateB, FiveElements timeA, FiveElements timeB)
    {
        int count = 0;

        if (element == yearA || element == yearB)
            count++;
        if (element == monthA || element == monthB)
            count++;
        if (element == dateA || element == dateB)
            count++;
        if (element == timeA || element == timeB)
            count++;

        return count;
    }
    
    public int CalculateMonth10(int selectedMonth, int year10)
    {
        int month10 = 0;

        switch (year10 % 5)
        {
            case 0:
                month10 = (selectedMonth + 7) % 10;
                break;
            case 1:
                month10 = (selectedMonth - 1) % 10;
                break;
            case 2:
                month10 = (selectedMonth + 1) % 10;
                break;
            case 3:
                month10 = (selectedMonth + 3) % 10;
                break;
            case 4:
                month10 = (selectedMonth + 5) % 10;
                break;
        }

        return month10;
    }
    
    public int CalculateDate10(int selectedDate, int year10)
    {
        int date10 = 0;

        switch (year10 % 5)
        {
            case 0:
                date10 = (selectedDate + 3) % 10;
                break;
            case 1:
                date10 = (selectedDate + 5) % 10;
                break;
            case 2:
                date10 = (selectedDate + 7) % 10;
                break;
            case 3:
                date10 = (selectedDate - 1) % 10;
                break;
            case 4:
                date10 = (selectedDate + 1) % 10;
                break;
        }

        return date10;
    }
    
    public int CalculateTime10(string selectedTime, int year10)
    {
        int temptime = 0;
        int hour = int.Parse(selectedTime.Split(':')[0]);

        if (hour >= 23 || hour < 1)
            temptime = 1;
        else if (hour >= 1 && hour < 3)
            temptime = 2;
        else if (hour >= 3 && hour < 5)
            temptime = 3;
        else if (hour >= 5 && hour < 7)
            temptime = 4;
        else if (hour >= 7 && hour < 9)
            temptime = 5;
        else if (hour >= 9 && hour < 11)
            temptime = 6;
        else if (hour >= 11 && hour < 13)
            temptime = 7;
        else if (hour >= 13 && hour < 15)
            temptime = 8;
        else if (hour >= 15 && hour < 17)
            temptime = 9;
        else if (hour >= 17 && hour < 19)
            temptime = 10;
        else if (hour >= 19 && hour < 21)
            temptime = 11;
        else if (hour >= 21 && hour < 23)
            temptime = 12;

        int time10 = 0;

        switch (year10 % 5)
        {
            case 0:
                time10 = (temptime - 1) % 10;
                break;
            case 1:
                time10 = (temptime + 1) % 10;
                break;
            case 2:
                time10 = (temptime + 3) % 10;
                break;
            case 3:
                time10 = (temptime + 5) % 10;
                break;
            case 4:
                time10 = (temptime + 7) % 10;
                break;
        }

        return time10;
    }
    
    public int CalculateTime12(string selectedTime)
    {
        int temptime = 0;
        int hour = int.Parse(selectedTime.Split(':')[0]);

        if (hour >= 23 || hour < 1)
            temptime = 1;
        else if (hour >= 1 && hour < 3)
            temptime = 2;
        else if (hour >= 3 && hour < 5)
            temptime = 3;
        else if (hour >= 5 && hour < 7)
            temptime = 4;
        else if (hour >= 7 && hour < 9)
            temptime = 5;
        else if (hour >= 9 && hour < 11)
            temptime = 6;
        else if (hour >= 11 && hour < 13)
            temptime = 7;
        else if (hour >= 13 && hour < 15)
            temptime = 8;
        else if (hour >= 15 && hour < 17)
            temptime = 9;
        else if (hour >= 17 && hour < 19)
            temptime = 10;
        else if (hour >= 19 && hour < 21)
            temptime = 11;
        else if (hour >= 21 && hour < 23)
            temptime = 12;

        int time12 = (temptime + 3) % 12;

        return time12;
    }

}
