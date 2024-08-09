namespace manage_endpoints.Model;

public class Endpoint
{
    public string SerialNumber { get; private set; }
    public int MeterModelId { get; private set; }
    public int MeterNumber { get; private set; }
    public string FirmwareVersion { get; private set; }
    public int SwitchState { get; private set; }

    public Endpoint(string serialNumber, int meterModelId, int meterNumber, string firmwareVersion, int switchState)
    {
        SerialNumber = serialNumber;
        MeterModelId = meterModelId;
        MeterNumber = meterNumber;
        FirmwareVersion = firmwareVersion;
        SwitchState = switchState;
    }

    public void UpdateSwitchState(int switchState)
    {
        if (switchState < 0 || switchState > 2)
        {
            throw new ArgumentException("Invalid switch state.");
        }

        SwitchState = switchState;
    }

    public override string ToString()
    {
        return
            $"Serial Number: {SerialNumber}, Meter Model Id: {MeterModelId}, Meter Number: {MeterNumber}, Firmware Version: {FirmwareVersion}, Switch State: {SwitchState}";
    }

}