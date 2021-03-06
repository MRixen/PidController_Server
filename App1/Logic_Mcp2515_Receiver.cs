﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanTest
{
    class Logic_Mcp2515_Receiver
    {
        private MCP2515 mcp2515;
        private GlobalDataSet globalDataSet;
        private Data_MCP2515_Receiver data_MCP2515_Receiver;
        private Stopwatch timeStopper = new Stopwatch();

        bool[] currentTxBuffer = { true, false, false };


        public Logic_Mcp2515_Receiver(GlobalDataSet globalDataSet)
        {
            this.globalDataSet = globalDataSet;
            mcp2515 = globalDataSet.Mcp2515;
            data_MCP2515_Receiver = new Data_MCP2515_Receiver();
        }

        public async void init_mcp2515_receiver_task()
        {
            await Task.Run(() => init_mcp2515_receiver());
        }

        public void init_mcp2515_receiver()
        {
            while (globalDataSet.Spi_not_initialized)
            {
                // Wait until spi is ready
            }

            // Reset chip to set in operation mode
            mcp2515_execute_reset_command();

            // Configure bit timing
            mcp2515_configureCanBus();

            // Configure interrupts
            mcp2515_configureInterrupts();

            // Configure bit masks and filters that we can receive everything 
            mcp2515_configureMasksFilters();

            // Set device to normal mode
            mcp2515_switchMode(mcp2515.CONTROL_REGISTER_CANSTAT_VALUE.NORMAL_MODE, mcp2515.CONTROL_REGISTER_CANCTRL_VALUE.NORMAL_MODE);

            // Init tx buffer (identifier, message size, etc.)
            mcp2515_init_tx_buffer0();
            mcp2515_init_tx_buffer1();
            mcp2515_init_tx_buffer2();
        }

        public void mcp2515_configureCanBus()
        {
            // Configure bit timing
            if(globalDataSet.DebugMode) Debug.Write("Configure bit timing for receiver" + "\n");
            byte[] spiMessage = new byte[2];          

            spiMessage[0] = mcp2515.CONTROL_REGISTER_CNF1;
            spiMessage[1] = mcp2515.CONTROL_REGISTER_CNFx_VALUE.CNF1;
            globalDataSet.mcp2515_execute_write_command(spiMessage, globalDataSet.do_mcp2515_cs_rec);

            spiMessage[0] = mcp2515.CONTROL_REGISTER_CNF2;
            spiMessage[1] = mcp2515.CONTROL_REGISTER_CNFx_VALUE.CNF2;
            globalDataSet.mcp2515_execute_write_command(spiMessage, globalDataSet.do_mcp2515_cs_rec);

            spiMessage[0] = mcp2515.CONTROL_REGISTER_CNF3;
            spiMessage[1] = mcp2515.CONTROL_REGISTER_CNFx_VALUE.CNF3;
            globalDataSet.mcp2515_execute_write_command(spiMessage, globalDataSet.do_mcp2515_cs_rec);
        }

        public void mcp2515_execute_reset_command()
        {
            // Reset chip to get initial condition and wait for operation mode state bit

            for (int i = 0; i < 3; i++)
            {
                if(globalDataSet.DebugMode) Debug.Write("Reset chip receiver " + i + "\n");
                byte[] returnMessage = new byte[1];

                globalDataSet.writeSimpleCommandSpi(mcp2515.SPI_INSTRUCTION_RESET, globalDataSet.do_mcp2515_cs_rec);

                // Read the register value
                byte actualMode = globalDataSet.mcp2515_execute_read_command(mcp2515.CONTROL_REGISTER_CANSTAT, globalDataSet.do_mcp2515_cs_rec);

                timeStopper.Reset();
                timeStopper.Start();
                while (((actualMode & mcp2515.CONTROL_REGISTER_CANSTAT_VALUE.CONFIGURATION_MODE) != mcp2515.CONTROL_REGISTER_CANSTAT_VALUE.CONFIGURATION_MODE) && (timeStopper.ElapsedMilliseconds <= globalDataSet.MAX_WAIT_TIME))
                {
                    actualMode = globalDataSet.mcp2515_execute_read_command(mcp2515.CONTROL_REGISTER_CANSTAT, globalDataSet.do_mcp2515_cs_rec);
                }
                if (timeStopper.ElapsedMilliseconds > globalDataSet.MAX_WAIT_TIME)
                {
                    if(globalDataSet.DebugMode) Debug.Write("Abort waiting for mode switch. Max. waiting time reached. Try again." + "\n");
                    // TODO Add to global error handling thread
                }
                else
                {
                    if(globalDataSet.DebugMode) Debug.Write("Switch receiver to mode " + actualMode.ToString() + " successfully" + "\n");
                    break;
                }
            }
        }

        public void mcp2515_switchMode(byte modeToCheck, byte modeToSwitch)
        {

            // Reset chip to get initial condition and wait for operation mode state bit
            byte[] spiMessage = new byte[] { mcp2515.CONTROL_REGISTER_CANCTRL, modeToSwitch };
            byte[] returnMessage = new byte[1];


            globalDataSet.mcp2515_execute_write_command(spiMessage, globalDataSet.do_mcp2515_cs_rec);

            // Read the register value
            byte actualMode = globalDataSet.mcp2515_execute_read_command(mcp2515.CONTROL_REGISTER_CANSTAT, globalDataSet.do_mcp2515_cs_rec);
            while (modeToCheck != (modeToCheck & actualMode))
            {
                actualMode = globalDataSet.mcp2515_execute_read_command(mcp2515.CONTROL_REGISTER_CANSTAT, globalDataSet.do_mcp2515_cs_rec);
            }
            if(globalDataSet.DebugMode) Debug.Write("Switch receiver to mode " + actualMode.ToString() + " successfully" + "\n");
        }

        private void mcp2515_configureMasksFilters()
        {
            if(globalDataSet.DebugMode) Debug.Write("Configure masks and filters for receiver" + "\n");
            byte[] spiMessage;

            // Set parameters for rx buffer 0
            spiMessage = new byte[] { mcp2515.CONTROL_REGISTER_RXB0CTRL, data_MCP2515_Receiver.CONTROL_REGISTER_RXB0CTRL_VALUE.RXB0CTRL };
            globalDataSet.mcp2515_execute_write_command(spiMessage, globalDataSet.do_mcp2515_cs_rec);

            // Set parameters for rx buffer 1
            spiMessage = new byte[] { mcp2515.CONTROL_REGISTER_RXB1CTRL, data_MCP2515_Receiver.CONTROL_REGISTER_RXB1CTRL_VALUE.RXB1CTRL };
            globalDataSet.mcp2515_execute_write_command(spiMessage, globalDataSet.do_mcp2515_cs_rec);
        }

        public void mcp2515_configureInterrupts()
        {
            if(globalDataSet.DebugMode) Debug.Write("Configure interrupts for receiver" + "\n");
            byte[] spiMessage = new byte[] { mcp2515.CONTROL_REGISTER_CANINTE, data_MCP2515_Receiver.CONTROL_REGISTER_CANINTE_VALUE.INTE };

            globalDataSet.mcp2515_execute_write_command(spiMessage, globalDataSet.do_mcp2515_cs_rec);
        }

        public byte mcp2515_read_buffer(byte byteId)
        {
            byte[] returnMessage = new byte[1];

            returnMessage = globalDataSet.readSimpleCommandSpi(byteId, globalDataSet.do_mcp2515_cs_rec);

            return returnMessage[0];
        }

        public byte[] mcp2515_read_buffer_v2(byte byteId)
        {
            byte[] returnMessage = new byte[7];

            returnMessage = globalDataSet.readSimpleCommandSpi_v2(byteId, globalDataSet.do_mcp2515_cs_rec);

            return returnMessage;
        }

        public byte[] mcp2515_read_buffer_v3(byte bufferId)
        {
            byte[] returnMessage = new byte[mcp2515.MessageSizePwm];

            returnMessage = globalDataSet.readSimpleCommandSpi_v3(bufferId, globalDataSet.do_mcp2515_cs_rec);

            return returnMessage;
        }

        public byte mcp2515_get_state_command()
        {
            return globalDataSet.executeReadStateCommand(globalDataSet.do_mcp2515_cs_rec);
        }

        public void mcp2515_load_tx_buffer0(byte byteId, byte data)
        {
            // Send message to mcp2515 tx buffer
            //if(globalDataSet.DebugMode) Debug.Write("Load tx buffer 0 at byte " + byteId.ToString() + " with " + data + "\n");
            byte[] spiMessage = new byte[2];

            // Set data to tx buffer 0
            spiMessage[0] = byteId;
            spiMessage[1] = data;
            globalDataSet.mcp2515_execute_write_command(spiMessage, globalDataSet.do_mcp2515_cs_rec);

            // Send message
            mcp2515_execute_rts_command(0);
        }

        public void mcp2515_load_tx_buffer0(byte messageData, int byteNumber, int messageSize)
        {            
            byte[] spiMessage = new byte[2];
            //int txBuffer_id = 0;
            //byte registerAddress = 0x00;

            //if (currentTxBuffer[0])
            //{
            //    registerAddress = mcp2515.REGISTER_TXB0Dx[byteNumber];
            //    txBuffer_id = 0;
            //    if (byteNumber == (messageSize -1))
            //    {
            //        currentTxBuffer[0] = false;
            //        currentTxBuffer[1] = true;
            //    }
            //}
            //else if (currentTxBuffer[1])
            //{
            //    registerAddress = mcp2515.REGISTER_TXB1Dx[byteNumber];
            //    txBuffer_id = 1;
            //    if (byteNumber == (messageSize - 1))
            //    {
            //        currentTxBuffer[1] = false;
            //        currentTxBuffer[2] = true;
            //    }
            //}
            //else if (currentTxBuffer[2])
            //{
            //    registerAddress = mcp2515.REGISTER_TXB2Dx[byteNumber];
            //    txBuffer_id = 2;
            //    if (byteNumber == (messageSize - 1))
            //    {
            //        currentTxBuffer[2] = false;
            //        currentTxBuffer[0] = true;
            //    }
            //}

            // Set data to tx buffer 
            spiMessage[0] = mcp2515.REGISTER_TXB0Dx[byteNumber];
            spiMessage[1] = messageData;

            globalDataSet.mcp2515_execute_write_command(spiMessage, globalDataSet.do_mcp2515_cs_rec);

            //Debug.WriteLine("currentTxBuffer_: " + txBuffer_id);
        }

        public void mcp2515_load_tx_buffer1(byte messageData, int byteNumber, int messageSize)
        {
            byte[] spiMessage = new byte[2];

            // Set data to tx buffer 
            spiMessage[0] = mcp2515.REGISTER_TXB1Dx[byteNumber];
            spiMessage[1] = messageData;

            globalDataSet.mcp2515_execute_write_command(spiMessage, globalDataSet.do_mcp2515_cs_rec);
        }

        public void mcp2515_load_tx_buffer2(byte messageData, int byteNumber, int messageSize)
        {
            byte[] spiMessage = new byte[2];

            // Set data to tx buffer 
            spiMessage[0] = mcp2515.REGISTER_TXB2Dx[byteNumber];
            spiMessage[1] = messageData;

            globalDataSet.mcp2515_execute_write_command(spiMessage, globalDataSet.do_mcp2515_cs_rec);
        }


        public void mcp2515_init_tx_buffer0()
        {
            byte[] spiMessage = new byte[2];

            // Set the message identifier to identifier[0] (low) and identifier[1] (high) and extended identifier to 0
            spiMessage[0] = mcp2515.REGISTER_TXB0SIDL;
            spiMessage[1] = mcp2515.REGISTER_TXB0SIDL_VALUE;
            globalDataSet.mcp2515_execute_write_command(spiMessage, globalDataSet.do_mcp2515_cs_rec);

            spiMessage[0] = mcp2515.REGISTER_TXB0SIDH;
            spiMessage[1] = mcp2515.REGISTER_TXB0SIDH_VALUE;
            globalDataSet.mcp2515_execute_write_command(spiMessage, globalDataSet.do_mcp2515_cs_rec);

            // Set data length and set rtr bit to zero (no remote request)
            spiMessage[0] = mcp2515.REGISTER_TXB0DLC;
            spiMessage[1] = mcp2515.MessageSizePwm;
            globalDataSet.mcp2515_execute_write_command(spiMessage, globalDataSet.do_mcp2515_cs_rec);
        }

        public void mcp2515_init_tx_buffer1()
        {
            byte[] spiMessage = new byte[2];

            // Set the message identifier to identifier[0] (low) and identifier[1] (high) and extended identifier to 0
            spiMessage[0] = mcp2515.REGISTER_TXB1SIDL;
            spiMessage[1] = mcp2515.REGISTER_TXB1SIDL_VALUE;
            globalDataSet.mcp2515_execute_write_command(spiMessage, globalDataSet.do_mcp2515_cs_rec);

            spiMessage[0] = mcp2515.REGISTER_TXB1SIDH;
            spiMessage[1] = mcp2515.REGISTER_TXB1SIDH_VALUE;
            globalDataSet.mcp2515_execute_write_command(spiMessage, globalDataSet.do_mcp2515_cs_rec);

            // Set data length and set rtr bit to zero (no remote request)
            spiMessage[0] = mcp2515.REGISTER_TXB1DLC;
            spiMessage[1] = mcp2515.MessageSizePwm;
            globalDataSet.mcp2515_execute_write_command(spiMessage, globalDataSet.do_mcp2515_cs_rec);
        }

        public void mcp2515_init_tx_buffer2()
        {
            byte[] spiMessage = new byte[2];

            // Set the message identifier to identifier[0] (low) and identifier[1] (high) and extended identifier to 0
            spiMessage[0] = mcp2515.REGISTER_TXB2SIDL;
            spiMessage[1] = mcp2515.REGISTER_TXB2SIDL_VALUE;
            globalDataSet.mcp2515_execute_write_command(spiMessage, globalDataSet.do_mcp2515_cs_rec);

            spiMessage[0] = mcp2515.REGISTER_TXB2SIDH;
            spiMessage[1] = mcp2515.REGISTER_TXB2SIDH_VALUE;
            globalDataSet.mcp2515_execute_write_command(spiMessage, globalDataSet.do_mcp2515_cs_rec);

            // Set data length and set rtr bit to zero (no remote request)
            spiMessage[0] = mcp2515.REGISTER_TXB2DLC;
            spiMessage[1] = mcp2515.MessageSizePwm;
            globalDataSet.mcp2515_execute_write_command(spiMessage, globalDataSet.do_mcp2515_cs_rec);
        }

        public void mcp2515_execute_rts_command(int bufferId)
        {
            byte[] spiMessage = new byte[1];
            switch (bufferId)
            {
                case 0:
                    spiMessage[0] = mcp2515.SPI_INSTRUCTION_RTS_BUFFER0;
                    break;
                case 1:
                    spiMessage[0] = mcp2515.SPI_INSTRUCTION_RTS_BUFFER1;
                    break;
                case 2:
                    spiMessage[0] = mcp2515.SPI_INSTRUCTION_RTS_BUFFER2;
                    break;
                default:
                    break;
            }

            globalDataSet.writeSimpleCommandSpi(spiMessage[0], globalDataSet.do_mcp2515_cs_rec);
        }

    }
}
