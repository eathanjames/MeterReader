﻿namespace Domain.Entities;

public class MeterReading
{
    public int AccountId { get; set; }
    public DateTime MeterReadingDateTime { get; set; }
    public string MeterReadValue { get; set; }
}