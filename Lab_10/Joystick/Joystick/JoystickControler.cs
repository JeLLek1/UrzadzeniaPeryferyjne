using System;
using System.Collections.Generic;
using SharpDX.DirectInput;

namespace JoystickProgram
{
    class JoystickControler
    {
        public static int analogMax = 100;
        Joystick joystick = null;
        JoystickState joystickState = new JoystickState();
        static double delta = 0.05; // bład odczytu

        public JoystickControler()
        {

        }

        public IDictionary<string, Guid> loadDevicesList()
        {
            IDictionary <string, Guid> gameControlers = new Dictionary<string, Guid>();
            gameControlers = new Dictionary<string, Guid>();
            gameControlers.Add("Wybierz urządzenie", Guid.Empty);
            DirectInput directInput = new DirectInput();
            foreach(DeviceInstance di in directInput.GetDevices(DeviceType.Gamepad, DeviceEnumerationFlags.AttachedOnly))
            {
                gameControlers.Add(di.InstanceName, di.InstanceGuid);
            }
            foreach (DeviceInstance di in directInput.GetDevices(DeviceType.Joystick, DeviceEnumerationFlags.AttachedOnly))
            {
                gameControlers.Add(di.InstanceName, di.InstanceGuid);
            }
            return gameControlers;
        }

        public bool isJoystickChoosen()
        {
            if (this.joystick == null) return false;
            return true;
        }

        public bool ChooseJoystick(Guid guid)
        {
            if(guid != Guid.Empty)
            {
                try
                {

                    DirectInput directInput = new DirectInput();
                    Joystick joystick = new Joystick(directInput, guid);
                    joystick.Properties.BufferSize = 128;
                    joystick.Acquire();

                    this.joystick = joystick;

                    this.joystick.Properties.Range = new InputRange(-analogMax, analogMax);
                    return true;
                }
                catch
                {

                }
            }
            return false;
        }

        public string GetName()
        {
            if(this.joystick == null)
            {
                return "Błąd";
            }
            else
            {
                return this.joystick.Properties.ProductName;
            }
        }

        public double[] GetRotationStick()
        {
            if (joystick == null) return null;
            JoystickState state = this.joystick.GetCurrentState();
            double[] array = new double[3];
            array[0] = (double)state.RotationX / analogMax;
            if (array[0] <= delta && array[0]>=-delta) array[0] = 0.0;
            array[1] = (double)state.RotationY / analogMax;
            if (array[1] <= delta && array[1] >= -delta) array[1] = 0.0;
            array[2] = (double)state.RotationZ / analogMax;
            if (array[2] <= delta && array[2] >= -delta) array[2] = 0.0;

            return array;
        }

        public double[] GetPositionStick()
        {
            if (joystick == null) return null;
            JoystickState state = this.joystick.GetCurrentState();
            double[] array = new double[3];
            array[0] = (double)state.X / analogMax;
            if (array[0] <= delta && array[0] >= -delta) array[0] = 0.0;
            array[1] = (double)state.Y / analogMax;
            if (array[1] <= delta && array[1] >= -delta) array[1] = 0.0;
            array[2] = (double)state.Z / analogMax;
            if (array[2] <= delta && array[2] >= -delta) array[2] = 0.0;

            return array;
        }

        public Dictionary<string, bool> GetButtonsPressed()
        {
            if (joystick == null) return null;
            JoystickState state = this.joystick.GetCurrentState();

            var map = new Dictionary<string, bool>();
            map.Add("A", state.Buttons[0]);
            map.Add("B", state.Buttons[1]);
            map.Add("X", state.Buttons[2]);
            map.Add("Y", state.Buttons[3]);
            map.Add("LB", state.Buttons[4]);
            map.Add("RB", state.Buttons[5]);
            map.Add("select", state.Buttons[6]);
            map.Add("start", state.Buttons[7]);
            map.Add("LSB", state.Buttons[8]);
            map.Add("RSB", state.Buttons[9]);
            map.Add("up", state.PointOfViewControllers[0] >= 0 && state.PointOfViewControllers[0] < 9000 || state.PointOfViewControllers[0]>27000 && state.PointOfViewControllers[0] <= 35999);
            map.Add("right", state.PointOfViewControllers[0] > 0 && state.PointOfViewControllers[0] < 18000);
            map.Add("down", state.PointOfViewControllers[0] > 9000 && state.PointOfViewControllers[0] < 27000);
            map.Add("left", state.PointOfViewControllers[0] > 18000 && state.PointOfViewControllers[0] <= 35999);

            return map;
        }

        public bool areNewEvents()
        {
            if (joystick == null) return false;
            joystick.Poll();
            var data = joystick.GetBufferedData();
            bool newEvents = false;
            foreach (var state in data)
            {
                newEvents = true;
            }
            return newEvents;
        }
    }
}
