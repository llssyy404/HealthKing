package com.example.administrator.healthking_teacher;

        import com.kr900l.ConnectSocket;
        import java.io.IOException;
        import java.io.InputStream;
        import java.io.OutputStream;
        import java.lang.reflect.InvocationTargetException;
        import java.lang.reflect.Method;

        import java.util.Set;
        import android.util.Log;

        import android.bluetooth.BluetoothAdapter;
        import android.bluetooth.BluetoothDevice;
        import android.bluetooth.BluetoothSocket;

/**
 * Created by Administrator on 2017-06-18.
 */

public class RFIDDevice {

    static private ConnectSocket _instance;
    static public ConnectSocket getInstance() {
        if (_instance == null) {
            _instance = new ConnectSocket();
        }
        return _instance;
    }

    static public ConnectSocket deviceEnum;

    public boolean Set() {

        try {
            if (_instance.ConnectReader() == false) {
                ConnectSocket.connectBluetoothState state = _instance.GetBluetoothState();

                if (state == ConnectSocket.connectBluetoothState.btsNotAvalibleBluetooth) {
                    //Toast.makeText(this, "Bluetooth is not available.", Toast.LENGTH_LONG).show();
                    //finish();
                    return true;
                } else if (state == ConnectSocket.connectBluetoothState.btsNotEnableBluetooth) {
                    //Intent enableBtIntenet = new Intent(BluetoothAdapter.ACTION_REQUEST_ENABLE);
                   // startActivityForResult(enableBtIntenet, REQUEST_ENABLE_BT);
                }

                //Toast.makeText(this, "Connection failure.", Toast.LENGTH_LONG).show();
            } else {
                //Toast.makeText(this, "Connection success.", Toast.LENGTH_LONG).show();
            }
        } catch (SecurityException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        } catch (IllegalArgumentException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        } catch (NoSuchMethodException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        } catch (IllegalAccessException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
        catch (InvocationTargetException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

		return  false;
    }

}
