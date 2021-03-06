﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanTest
{
    class MCP2515
    {
        // CONTROL REGISTER
        private byte cONTROL_REGISTER_BFPCTRL = 0x0C;
        private byte cONTROL_REGISTER_TXRTSCTRL = 0x0D;
        private byte cONTROL_REGISTER_CANSTAT = 0x0E;
        private byte cONTROL_REGISTER_CANCTRL = 0x0F;
        private byte cONTROL_REGISTER_TEC = 0x1C;
        private byte cONTROL_REGISTER_REGISTER_REC = 0x1D;
        private byte cONTROL_REGISTER_CNF1 = 0x2A;
        private byte cONTROL_REGISTER_CNF2 = 0x29;
        private byte cONTROL_REGISTER_CNF3 = 0x28;
        private byte cONTROL_REGISTER_CANINTE = 0x2B;
        private byte cONTROL_REGISTER_CANINTF = 0x2C;
        private byte cONTROL_REGISTER_EFLG = 0x2D;
        private byte cONTROL_REGISTER_TXB0CTRL = 0x30;
        private byte cONTROL_REGISTER_TXB1CTRL = 0x40;
        private byte cONTROL_REGISTER_TXB2CTRL = 0x50;
        private byte cONTROL_REGISTER_RXB0CTRL = 0x60;
        private byte cONTROL_REGISTER_RXB1CTRL = 0x70;

        // REGISTER
        private byte rEGISTER_TXB0SIDH = 0x31;
        private byte rEGISTER_TXB0SIDL = 0x32;
        private byte rEGISTER_TXB0DLC = 0x35;
        private byte rEGISTER_TXB0D0 = 0x36;
        private byte rEGISTER_TXB0D1 = 0x37;
        private byte rEGISTER_TXB0D2 = 0x38;
        private byte rEGISTER_TXB0D3 = 0x39;
        private byte rEGISTER_TXB0D4 = 0x3A;
        private byte rEGISTER_TXB0D5 = 0x3B;
        private byte rEGISTER_TXB0D6 = 0x3C;
        private byte rEGISTER_TXB0D7 = 0x3D;

        private byte rEGISTER_TXB1SIDH = 0x41;
        private byte rEGISTER_TXB1SIDL = 0x42;
        private byte rEGISTER_TXB1DLC = 0x45;
        private byte rEGISTER_TXB1D0 = 0x46;
        private byte rEGISTER_TXB1D1 = 0x47;
        private byte rEGISTER_TXB1D2 = 0x48;
        private byte rEGISTER_TXB1D3 = 0x49;
        private byte rEGISTER_TXB1D4 = 0x4A;
        private byte rEGISTER_TXB1D5 = 0x4B;
        private byte rEGISTER_TXB1D6 = 0x4C;
        private byte rEGISTER_TXB1D7 = 0x4D;

        private byte rEGISTER_TXB2SIDH = 0x51;
        private byte rEGISTER_TXB2SIDL = 0x52;
        private byte rEGISTER_TXB2DLC = 0x55;
        private byte rEGISTER_TXB2D0 = 0x56;
        private byte rEGISTER_TXB2D1 = 0x57;
        private byte rEGISTER_TXB2D2 = 0x58;
        private byte rEGISTER_TXB2D3 = 0x59;
        private byte rEGISTER_TXB2D4 = 0x5A;
        private byte rEGISTER_TXB2D5 = 0x5B;
        private byte rEGISTER_TXB2D6 = 0x5C;
        private byte rEGISTER_TXB2D7 = 0x5D;


        private byte rEGISTER_RXB0SIDH = 0x61;
        private byte rEGISTER_RXB0SIDL = 0x62;
        private byte rEGISTER_RXB0D0 = 0x66;
        private byte rEGISTER_RXB0D1 = 0x67;
        private byte rEGISTER_RXB0D2 = 0x68;
        private byte rEGISTER_RXB0D3 = 0x69;
        private byte rEGISTER_RXB0D4 = 0x6A;
        private byte rEGISTER_RXB0D5 = 0x6B;
        private byte rEGISTER_RXB0D6 = 0x6C;
        private byte rEGISTER_RXB0D7 = 0x6D;

        private byte rEGISTER_RXB1SIDH = 0x71;
        private byte rEGISTER_RXB1SIDL = 0x72;
        private byte rEGISTER_RXB1D0 = 0x76;
        private byte rEGISTER_RXB1D1 = 0x77;
        private byte rEGISTER_RXB1D2 = 0x78;
        private byte rEGISTER_RXB1D3 = 0x79;
        private byte rEGISTER_RXB1D4 = 0x7A;
        private byte rEGISTER_RXB1D5 = 0x7B;
        private byte rEGISTER_RXB1D6 = 0x7C;
        private byte rEGISTER_RXB1D7 = 0x7D;

        // SPI INSTRUCTIONS
        private byte sPI_INSTRUCTION_RESET = 0xC0;
        private byte sPI_INSTRUCTION_READ = 0x03;
        private byte sPI_INSTRUCTION_READ_RX_BUFFER0_SIDH = 0x90;
        private byte sPI_INSTRUCTION_READ_RX_BUFFER0 = 0x92;
        private byte sPI_INSTRUCTION_READ_RX_BUFFER1 = 0x96;
        private byte sPI_INSTRUCTION_WRITE = 0x02;
        private byte sPI_INSTRUCTION_LOAD_TX_BUFFER0_ID = 0x40;
        private byte sPI_INSTRUCTION_LOAD_TX_BUFFER0_DATA = 0x41;
        private byte sPI_INSTRUCTION_RTS_BUFFER0 = 0x81;
        private byte sPI_INSTRUCTION_RTS_BUFFER1 = 0x82;
        private byte sPI_INSTRUCTION_RTS_BUFFER2 = 0x84;
        private byte sPI_INSTRUCTION_READ_STATUS = 0xA0;
        private byte sPI_INSTRUCTION_RX_STATUS = 0xB0;
        private byte sPI_INSTRUCTION_BIT_MODIFY = 0x05;

        // GLOBAL DATA
        private cONTROL_REGISTER_CNFx_VALUE control_register_cnfx_value;
        private cONTROL_REGISTER_CANSTAT_VALUE control_register_canstat_value;
        private cONTROL_REGISTER_CANCTRL_VALUE control_register_canctrl_value;
        private cONTROL_REGISTER_CANINTF_VALUE control_register_canintf_value;

        // DEVICE 0
        private byte rEGISTER_TXB0SIDL_VALUE = 0x20;
        private byte rEGISTER_TXB0SIDH_VALUE = 0x01;
        private byte rEGISTER_TXB1SIDL_VALUE = 0x20;
        private byte rEGISTER_TXB1SIDH_VALUE = 0x01;
        private byte rEGISTER_TXB2SIDL_VALUE = 0x20;
        private byte rEGISTER_TXB2SIDH_VALUE = 0x01;

        private byte[] rEGISTER_TXB0Dx = new byte[8];
        private byte[] rEGISTER_TXB1Dx = new byte[8];
        private byte[] rEGISTER_TXB2Dx = new byte[8];
        private byte[] rEGISTER_RXB0Dx = new byte[8];
        private byte[] rEGISTER_RXB1Dx = new byte[8];
        private byte messageSizeAdxl, messageSizePwm;

        public struct cONTROL_REGISTER_CANSTAT_VALUE
        {
            public byte NORMAL_MODE;
            public byte NORMAL_MODE_WITH_OSM;
            public byte SLEEP_MODE;
            public byte LOOPBACK_MODE;
            public byte LISTEN_ONLY_MODE;
            public byte CONFIGURATION_MODE;
        }
        public struct cONTROL_REGISTER_CANCTRL_VALUE
        {
            public byte NORMAL_MODE;
            public byte NORMAL_MODE_WITH_OSM;
            public byte SLEEP_MODE;
            public byte LOOPBACK_MODE;
            public byte LISTEN_ONLY_MODE;
            public byte CONFIGURATION_MODE;
        }

        public struct cONTROL_REGISTER_CANINTF_VALUE
        {
            public byte RESET_RX0IF;
            public byte RESET_RX1IF;
            public byte RESET_TX0IF;
            public byte RESET_TX1IF;
            public byte RESET_TX2IF;
            public byte RESET_ERRIF;
            public byte RESET_WAKIF;
            public byte RESET_MERRIF;
            public byte RESET_ALL_IF;
        }

        public struct cONTROL_REGISTER_CNFx_VALUE
        {
            public byte CNF1;
            public byte CNF2;
            public byte CNF3;
        }


        public struct cONTROL_REGISTER_TXB0CTRL_VALUE
        {
            public byte TXB0CTRL;
        }


        public MCP2515()
        {
            configureGlobalData();
            configureUserData();
        }

        private void configureUserData()
        {
            control_register_cnfx_value.CNF1 = 0x00;// 0x03; // Baud rate prescaler
            control_register_cnfx_value.CNF2 = 0xB8;// 0x90; // BTLMODE = 1 and PhaseSegment1 = 2
            control_register_cnfx_value.CNF3 = 0x05;// 0x02; // PhaseSegment2 = 2
        }

        private void configureGlobalData()
        {
            // Set values for mode state
            control_register_canstat_value.NORMAL_MODE = 0x00;
            control_register_canctrl_value.NORMAL_MODE_WITH_OSM = 0x08;
            control_register_canstat_value.SLEEP_MODE = 0x20;
            control_register_canstat_value.LOOPBACK_MODE = 0x40;
            control_register_canstat_value.LISTEN_ONLY_MODE = 0x60;
            control_register_canstat_value.CONFIGURATION_MODE = 0x80;

            // Set values for mode control
            control_register_canctrl_value.NORMAL_MODE = 0x00;
            control_register_canctrl_value.NORMAL_MODE_WITH_OSM = 0x08;
            control_register_canctrl_value.SLEEP_MODE = 0x20;
            control_register_canctrl_value.LOOPBACK_MODE = 0x40;
            control_register_canctrl_value.LISTEN_ONLY_MODE = 0x60;
            control_register_canctrl_value.CONFIGURATION_MODE = 0x80;

            // Set values for message size
            MessageSizeAdxl = 0x07; // Sensor data + identifier (1 byte)
            messageSizePwm = 0x03;

            // Set addresss for tx buffer 0
            rEGISTER_TXB0Dx[0] = rEGISTER_TXB0D0;
            rEGISTER_TXB0Dx[1] = rEGISTER_TXB0D1;
            rEGISTER_TXB0Dx[2] = rEGISTER_TXB0D2;
            rEGISTER_TXB0Dx[3] = rEGISTER_TXB0D3;
            rEGISTER_TXB0Dx[4] = rEGISTER_TXB0D4;
            rEGISTER_TXB0Dx[5] = rEGISTER_TXB0D5;
            rEGISTER_TXB0Dx[6] = rEGISTER_TXB0D6;
            rEGISTER_TXB0Dx[7] = rEGISTER_TXB0D7;

            // Set addresss for tx buffer 1
            rEGISTER_TXB1Dx[0] = rEGISTER_TXB1D0;
            rEGISTER_TXB1Dx[1] = rEGISTER_TXB1D1;
            rEGISTER_TXB1Dx[2] = rEGISTER_TXB1D2;
            rEGISTER_TXB1Dx[3] = rEGISTER_TXB1D3;
            rEGISTER_TXB1Dx[4] = rEGISTER_TXB1D4;
            rEGISTER_TXB1Dx[5] = rEGISTER_TXB1D5;
            rEGISTER_TXB1Dx[6] = rEGISTER_TXB1D6;
            rEGISTER_TXB1Dx[7] = rEGISTER_TXB1D7;

            // Set addresss for tx buffer 2
            rEGISTER_TXB2Dx[0] = rEGISTER_TXB2D0;
            rEGISTER_TXB2Dx[1] = rEGISTER_TXB2D1;
            rEGISTER_TXB2Dx[2] = rEGISTER_TXB2D2;
            rEGISTER_TXB2Dx[3] = rEGISTER_TXB2D3;
            rEGISTER_TXB2Dx[4] = rEGISTER_TXB2D4;
            rEGISTER_TXB2Dx[5] = rEGISTER_TXB2D5;
            rEGISTER_TXB2Dx[6] = rEGISTER_TXB2D6;
            rEGISTER_TXB2Dx[7] = rEGISTER_TXB2D7;

            // Set addresss for rx buffer 0
            rEGISTER_RXB0Dx[0] = rEGISTER_RXB0D0;
            rEGISTER_RXB0Dx[1] = rEGISTER_RXB0D1;
            rEGISTER_RXB0Dx[2] = rEGISTER_RXB0D2;
            rEGISTER_RXB0Dx[3] = rEGISTER_RXB0D3;
            rEGISTER_RXB0Dx[4] = rEGISTER_RXB0D4;
            rEGISTER_RXB0Dx[5] = rEGISTER_RXB0D5;
            rEGISTER_RXB0Dx[6] = rEGISTER_RXB0D6;
            rEGISTER_RXB0Dx[7] = rEGISTER_RXB0D7;

            // Set addresss for rx buffer 1
            rEGISTER_RXB1Dx[0] = rEGISTER_RXB1D0;
            rEGISTER_RXB1Dx[1] = rEGISTER_RXB1D1;
            rEGISTER_RXB1Dx[2] = rEGISTER_RXB1D2;
            rEGISTER_RXB1Dx[3] = rEGISTER_RXB1D3;
            rEGISTER_RXB1Dx[4] = rEGISTER_RXB1D4;
            rEGISTER_RXB1Dx[5] = rEGISTER_RXB1D5;
            rEGISTER_RXB1Dx[6] = rEGISTER_RXB1D6;
            rEGISTER_RXB1Dx[7] = rEGISTER_RXB1D7;

            // Set data for interrupt flags
            control_register_canintf_value.RESET_ALL_IF = 0x00;
            control_register_canintf_value.RESET_MERRIF = 0x7F;
            control_register_canintf_value.RESET_WAKIF = 0xBF;
            control_register_canintf_value.RESET_ERRIF = 0xDF;
            control_register_canintf_value.RESET_TX2IF = 0xEF;
            control_register_canintf_value.RESET_TX1IF = 0xF7;
            control_register_canintf_value.RESET_TX0IF = 0xFB;
            control_register_canintf_value.RESET_RX1IF = 0xFD;
            control_register_canintf_value.RESET_RX0IF = 0xFE;
        }

        public byte CONTROL_REGISTER_BFPCTRL
        {
            get
            {
                return cONTROL_REGISTER_BFPCTRL;
            }

        }

        public byte CONTROL_REGISTER_TXRTSCTRL
        {
            get
            {
                return cONTROL_REGISTER_TXRTSCTRL;
            }

        }

        public byte CONTROL_REGISTER_CANSTAT
        {
            get
            {
                return cONTROL_REGISTER_CANSTAT;
            }

            set
            {
                cONTROL_REGISTER_CANSTAT = value;
            }
        }

        public byte CONTROL_REGISTER_CANCTRL
        {
            get
            {
                return cONTROL_REGISTER_CANCTRL;
            }

            set
            {
                cONTROL_REGISTER_CANCTRL = value;
            }
        }

        public byte CONTROL_REGISTER_TEC
        {
            get
            {
                return cONTROL_REGISTER_TEC;
            }

            set
            {
                cONTROL_REGISTER_TEC = value;
            }
        }

        public byte CONTROL_REGISTER_REGISTER_REC
        {
            get
            {
                return cONTROL_REGISTER_REGISTER_REC;
            }

            set
            {
                cONTROL_REGISTER_REGISTER_REC = value;
            }
        }

        public byte CONTROL_REGISTER_CNF3
        {
            get
            {
                return cONTROL_REGISTER_CNF3;
            }

            set
            {
                cONTROL_REGISTER_CNF3 = value;
            }
        }

        public byte CONTROL_REGISTER_CNF2
        {
            get
            {
                return cONTROL_REGISTER_CNF2;
            }

            set
            {
                cONTROL_REGISTER_CNF2 = value;
            }
        }

        public byte CONTROL_REGISTER_CNF1
        {
            get
            {
                return cONTROL_REGISTER_CNF1;
            }

            set
            {
                cONTROL_REGISTER_CNF1 = value;
            }
        }

        public byte CONTROL_REGISTER_CANINTE
        {
            get
            {
                return cONTROL_REGISTER_CANINTE;
            }

            set
            {
                cONTROL_REGISTER_CANINTE = value;
            }
        }

        public byte CONTROL_REGISTER_CANINTF
        {
            get
            {
                return cONTROL_REGISTER_CANINTF;
            }

            set
            {
                cONTROL_REGISTER_CANINTF = value;
            }
        }

        public byte CONTROL_REGISTER_EFLG
        {
            get
            {
                return cONTROL_REGISTER_EFLG;
            }

            set
            {
                cONTROL_REGISTER_EFLG = value;
            }
        }

        public byte CONTROL_REGISTER_TXB0CTRL
        {
            get
            {
                return cONTROL_REGISTER_TXB0CTRL;
            }

            set
            {
                cONTROL_REGISTER_TXB0CTRL = value;
            }
        }

        public byte CONTROL_REGISTER_TXB1CTRL
        {
            get
            {
                return cONTROL_REGISTER_TXB1CTRL;
            }

            set
            {
                cONTROL_REGISTER_TXB1CTRL = value;
            }
        }

        public byte CONTROL_REGISTER_TXB2CTRL
        {
            get
            {
                return cONTROL_REGISTER_TXB2CTRL;
            }

            set
            {
                cONTROL_REGISTER_TXB2CTRL = value;
            }
        }

        public byte CONTROL_REGISTER_RXB0CTRL
        {
            get
            {
                return cONTROL_REGISTER_RXB0CTRL;
            }

            set
            {
                cONTROL_REGISTER_RXB0CTRL = value;
            }
        }

        public byte CONTROL_REGISTER_RXB1CTRL
        {
            get
            {
                return cONTROL_REGISTER_RXB1CTRL;
            }

            set
            {
                cONTROL_REGISTER_RXB1CTRL = value;
            }
        }

        public byte SPI_INSTRUCTION_RESET
        {
            get
            {
                return sPI_INSTRUCTION_RESET;
            }

            set
            {
                sPI_INSTRUCTION_RESET = value;
            }
        }

        public byte SPI_INSTRUCTION_READ
        {
            get
            {
                return sPI_INSTRUCTION_READ;
            }

            set
            {
                sPI_INSTRUCTION_READ = value;
            }
        }

        public byte SPI_INSTRUCTION_WRITE
        {
            get
            {
                return sPI_INSTRUCTION_WRITE;
            }

            set
            {
                sPI_INSTRUCTION_WRITE = value;
            }
        }

        public byte SPI_INSTRUCTION_READ_STATUS
        {
            get
            {
                return sPI_INSTRUCTION_READ_STATUS;
            }

            set
            {
                sPI_INSTRUCTION_READ_STATUS = value;
            }
        }

        public byte SPI_INSTRUCTION_RX_STATUS
        {
            get
            {
                return sPI_INSTRUCTION_RX_STATUS;
            }

            set
            {
                sPI_INSTRUCTION_RX_STATUS = value;
            }
        }

        public byte SPI_INSTRUCTION_BIT_MODIFY
        {
            get
            {
                return sPI_INSTRUCTION_BIT_MODIFY;
            }

            set
            {
                sPI_INSTRUCTION_BIT_MODIFY = value;
            }
        }

        public cONTROL_REGISTER_CNFx_VALUE CONTROL_REGISTER_CNFx_VALUE
        {
            get
            {
                return control_register_cnfx_value;
            }

            set
            {
                control_register_cnfx_value = value;
            }
        }

        public cONTROL_REGISTER_CANSTAT_VALUE CONTROL_REGISTER_CANSTAT_VALUE
        {
            get
            {
                return control_register_canstat_value;
            }

            set
            {
                control_register_canstat_value = value;
            }
        }

        public cONTROL_REGISTER_CANCTRL_VALUE CONTROL_REGISTER_CANCTRL_VALUE
        {
            get
            {
                return control_register_canctrl_value;
            }

            set
            {
                control_register_canctrl_value = value;
            }
        }

        public byte REGISTER_TXB0SIDH
        {
            get
            {
                return rEGISTER_TXB0SIDH;
            }

            set
            {
                rEGISTER_TXB0SIDH = value;
            }
        }

        public byte REGISTER_TXB0SIDL
        {
            get
            {
                return rEGISTER_TXB0SIDL;
            }

            set
            {
                rEGISTER_TXB0SIDL = value;
            }
        }

        public byte REGISTER_TXB0DLC
        {
            get
            {
                return rEGISTER_TXB0DLC;
            }

            set
            {
                rEGISTER_TXB0DLC = value;
            }
        }

        public byte REGISTER_TXB1SIDH
        {
            get
            {
                return rEGISTER_TXB1SIDH;
            }

            set
            {
                rEGISTER_TXB1SIDH = value;
            }
        }

        public byte REGISTER_TXB1SIDL
        {
            get
            {
                return rEGISTER_TXB1SIDL;
            }

            set
            {
                rEGISTER_TXB1SIDL = value;
            }
        }

        public byte REGISTER_TXB1DLC
        {
            get
            {
                return rEGISTER_TXB1DLC;
            }

            set
            {
                rEGISTER_TXB1DLC = value;
            }
        }

        public byte REGISTER_TXB2SIDH
        {
            get
            {
                return rEGISTER_TXB2SIDH;
            }

            set
            {
                rEGISTER_TXB2SIDH = value;
            }
        }

        public byte REGISTER_TXB2SIDL
        {
            get
            {
                return rEGISTER_TXB2SIDL;
            }

            set
            {
                rEGISTER_TXB2SIDL = value;
            }
        }

        public byte REGISTER_TXB2DLC
        {
            get
            {
                return rEGISTER_TXB2DLC;
            }

            set
            {
                rEGISTER_TXB2DLC = value;
            }
        }

        public byte REGISTER_RXB0SIDH
        {
            get
            {
                return rEGISTER_RXB0SIDH;
            }

            set
            {
                rEGISTER_RXB0SIDH = value;
            }
        }

        public byte REGISTER_RXB0SIDL
        {
            get
            {
                return rEGISTER_RXB0SIDL;
            }

            set
            {
                rEGISTER_RXB0SIDL = value;
            }
        }

        public byte REGISTER_RXB1SIDH
        {
            get
            {
                return rEGISTER_RXB1SIDH;
            }

            set
            {
                rEGISTER_RXB1SIDH = value;
            }
        }

        public byte REGISTER_RXB1SIDL
        {
            get
            {
                return rEGISTER_RXB1SIDL;
            }

            set
            {
                rEGISTER_RXB1SIDL = value;
            }
        }



        public byte REGISTER_TXB0SIDL_VALUE
        {
            get
            {
                return rEGISTER_TXB0SIDL_VALUE;
            }

            set
            {
                rEGISTER_TXB0SIDL_VALUE = value;
            }
        }

        public byte REGISTER_TXB0SIDH_VALUE
        {
            get
            {
                return rEGISTER_TXB0SIDH_VALUE;
            }

            set
            {
                rEGISTER_TXB0SIDH_VALUE = value;
            }
        }

        //--
        public byte REGISTER_TXB1SIDL_VALUE
        {
            get
            {
                return rEGISTER_TXB1SIDL_VALUE;
            }

            set
            {
                rEGISTER_TXB1SIDL_VALUE = value;
            }
        }

        public byte REGISTER_TXB1SIDH_VALUE
        {
            get
            {
                return rEGISTER_TXB1SIDH_VALUE;
            }

            set
            {
                rEGISTER_TXB1SIDH_VALUE = value;
            }
        }
        public byte REGISTER_TXB2SIDL_VALUE
        {
            get
            {
                return rEGISTER_TXB2SIDL_VALUE;
            }

            set
            {
                rEGISTER_TXB2SIDL_VALUE = value;
            }
        }

        public byte REGISTER_TXB2SIDH_VALUE
        {
            get
            {
                return rEGISTER_TXB2SIDH_VALUE;
            }

            set
            {
                rEGISTER_TXB2SIDH_VALUE = value;
            }
        }

        //--

        public byte SPI_INSTRUCTION_RTS_BUFFER0
        {
            get
            {
                return sPI_INSTRUCTION_RTS_BUFFER0;
            }

            set
            {
                sPI_INSTRUCTION_RTS_BUFFER0 = value;
            }
        }

        public byte SPI_INSTRUCTION_RTS_BUFFER1
        {
            get
            {
                return sPI_INSTRUCTION_RTS_BUFFER1;
            }

            set
            {
                sPI_INSTRUCTION_RTS_BUFFER1 = value;
            }
        }

        public byte SPI_INSTRUCTION_RTS_BUFFER2
        {
            get
            {
                return sPI_INSTRUCTION_RTS_BUFFER2;
            }

            set
            {
                sPI_INSTRUCTION_RTS_BUFFER2 = value;
            }
        }

        public byte[] REGISTER_TXB0Dx
        {
            get
            {
                return rEGISTER_TXB0Dx;
            }

            set
            {
                rEGISTER_TXB0Dx = value;
            }
        }

        public byte[] REGISTER_TXB1Dx
        {
            get
            {
                return rEGISTER_TXB1Dx;
            }

            set
            {
                rEGISTER_TXB1Dx = value;
            }
        }

        public byte[] REGISTER_TXB2Dx
        {
            get
            {
                return rEGISTER_TXB2Dx;
            }

            set
            {
                rEGISTER_TXB2Dx = value;
            }
        }

        public byte[] REGISTER_RXB0Dx
        {
            get
            {
                return rEGISTER_RXB0Dx;
            }

            set
            {
                rEGISTER_RXB0Dx = value;
            }
        }

        public byte[] REGISTER_RXB1Dx
        {
            get
            {
                return rEGISTER_RXB1Dx;
            }

            set
            {
                rEGISTER_RXB1Dx = value;
            }
        }

        public byte SPI_INSTRUCTION_LOAD_TX_BUFFER0_ID
        {
            get
            {
                return sPI_INSTRUCTION_LOAD_TX_BUFFER0_ID;
            }

            set
            {
                sPI_INSTRUCTION_LOAD_TX_BUFFER0_ID = value;
            }
        }

        public byte SPI_INSTRUCTION_LOAD_TX_BUFFER0_DATA
        {
            get
            {
                return sPI_INSTRUCTION_LOAD_TX_BUFFER0_DATA;
            }

            set
            {
                sPI_INSTRUCTION_LOAD_TX_BUFFER0_DATA = value;
            }
        }

        public byte SPI_INSTRUCTION_READ_RX_BUFFER0_SIDH
        {
            get
            {
                return sPI_INSTRUCTION_READ_RX_BUFFER0_SIDH;
            }

            set
            {
                sPI_INSTRUCTION_READ_RX_BUFFER0_SIDH = value;
            }
        }

        public byte SPI_INSTRUCTION_READ_RX_BUFFER0
        {
            get
            {
                return sPI_INSTRUCTION_READ_RX_BUFFER0;
            }

            set
            {
                sPI_INSTRUCTION_READ_RX_BUFFER0 = value;
            }
        }

        public byte SPI_INSTRUCTION_READ_RX_BUFFER1
        {
            get
            {
                return sPI_INSTRUCTION_READ_RX_BUFFER1;
            }

            set
            {
                sPI_INSTRUCTION_READ_RX_BUFFER1 = value;
            }
        }

        public cONTROL_REGISTER_CANINTF_VALUE CONTROL_REGISTER_CANINTF_VALUE
        {
            get
            {
                return control_register_canintf_value;
            }

            set
            {
                control_register_canintf_value = value;
            }
        }

        public byte MessageSizeAdxl
        {
            get
            {
                return messageSizeAdxl;
            }

            set
            {
                messageSizeAdxl = value;
            }
        }

        public byte MessageSizePwm
        {
            get
            {
                return messageSizePwm;
            }

            set
            {
                messageSizePwm = value;
            }
        }
    }
}
