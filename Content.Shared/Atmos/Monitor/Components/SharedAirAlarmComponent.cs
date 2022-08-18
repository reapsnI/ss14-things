using Robust.Shared.Serialization;

namespace Content.Shared.Atmos.Monitor.Components
{
    [Serializable, NetSerializable]
    public enum SharedAirAlarmInterfaceKey
    {
        Key
    }

    [Serializable, NetSerializable]
    public enum AirAlarmMode
    {
        None,
        Filtering,
        Fill,
        Panic,
        Replace
    }

    [Serializable, NetSerializable]
    public enum AirAlarmWireStatus
    {
        Power,
        Access,
        Panic,
        DeviceSync
    }

    [Serializable, NetSerializable]
    public readonly struct AirAlarmAirData
    {
        public readonly float? Pressure { get; }
        public readonly float? Temperature { get; }
        public readonly float? TotalMoles { get; }
        public readonly AtmosMonitorAlarmType AlarmState { get; }

        private readonly Dictionary<Gas, float>? _gases;
        public readonly IReadOnlyDictionary<Gas, float>? Gases { get => _gases; }

        public AirAlarmAirData(float? pressure, float? temperature, float? moles, AtmosMonitorAlarmType state, Dictionary<Gas, float>? gases)
        {
            Pressure = pressure;
            Temperature = temperature;
            TotalMoles = moles;
            AlarmState = state;
            _gases = gases;
        }
    }

    public interface IAtmosDeviceData
    {
        public bool Enabled { get; set; }
        public bool Dirty { get; set; }
        public bool IgnoreAlarms { get; set; }
    }

    [Serializable, NetSerializable]
    public sealed class AirAlarmUIState : BoundUserInterfaceState
    {
        public AirAlarmUIState(Dictionary<string, IAtmosDeviceData> deviceData, AirAlarmMode mode, AirAlarmTab tab, AtmosMonitorAlarmType alarmType)
        {
            DeviceData = deviceData;
            Mode = mode;
            Tab = tab;
            AlarmType = alarmType;
        }

        /// <summary>
        ///     Every single device data that can be seen from this
        ///     air alarm. This includes vents, scrubbers, and sensors.
        ///     The device data you get, however, depends on the current
        ///     selected tab.
        /// </summary>
        public Dictionary<string, IAtmosDeviceData> DeviceData { get; }
        public AirAlarmMode Mode { get; }
        public AirAlarmTab Tab { get; }
        public AtmosMonitorAlarmType AlarmType { get; }
    }

    [Serializable, NetSerializable]
    public sealed class AirAlarmTabSetMessage : BoundUserInterfaceMessage
    {
        public AirAlarmTab Tab { get; }
    }

    [Serializable, NetSerializable]
    public sealed class AirAlarmResyncAllDevicesMessage : BoundUserInterfaceMessage
    {}

    [Serializable, NetSerializable]
    public sealed class AirAlarmSetAddressMessage : BoundUserInterfaceMessage
    {
        public string Address { get; }

        public AirAlarmSetAddressMessage(string address)
        {
            Address = address;
        }
    }

    [Serializable, NetSerializable]
    public sealed class AirAlarmUpdateAirDataMessage : BoundUserInterfaceMessage
    {
        public AirAlarmAirData AirData;

        public AirAlarmUpdateAirDataMessage(AirAlarmAirData airData)
        {
            AirData = airData;
        }
    }

    [Serializable, NetSerializable]
    public sealed class AirAlarmUpdateAlarmModeMessage : BoundUserInterfaceMessage
    {
        public AirAlarmMode Mode { get; }

        public AirAlarmUpdateAlarmModeMessage(AirAlarmMode mode)
        {
            Mode = mode;
        }
    }

    [Serializable, NetSerializable]
    public sealed class AirAlarmUpdateDeviceDataMessage : BoundUserInterfaceMessage
    {
        public string Address { get; }
        public IAtmosDeviceData Data { get; }

        public AirAlarmUpdateDeviceDataMessage(string addr, IAtmosDeviceData data)
        {
            Address = addr;
            Data = data;
        }
    }

    [Serializable, NetSerializable]
    public sealed class AirAlarmUpdateAlarmThresholdMessage : BoundUserInterfaceMessage
    {
        public AtmosAlarmThreshold Threshold { get; }
        public AtmosMonitorThresholdType Type { get; }
        public Gas? Gas { get; }

        public AirAlarmUpdateAlarmThresholdMessage(AtmosMonitorThresholdType type, AtmosAlarmThreshold threshold, Gas? gas = null)
        {
            Threshold = threshold;
            Type = type;
            Gas = gas;
        }
    }

    public enum AirAlarmTab
    {
        Vent,
        Scrubber,
        Sensors,
        Settings
    }
}
