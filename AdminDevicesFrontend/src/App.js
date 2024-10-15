import React, { useState, useEffect } from 'react';
import axios from 'axios';
import AddDevice from './Components/AddDevice/AddDevice';
import DeviceList from './Components/DeviceList/DeviceList';

const App = () => {
  const [devices, setDevices] = useState([]);
  const [selectedDeviceId, setSelectedDeviceId] = useState(null);
  const [editMode, setEditMode] = useState(false);

  useEffect(() => {
    axios.get('http://localhost:5220/api/device')
      .then(response => setDevices(response.data))
      .catch(error => console.error('There was an error fetching the devices!', error));
  }, []);

  // Add a new device to the list
  const handleAddDevice = (newDevice) => {
    setDevices([...devices, newDevice]); // Update the device list with the new device
  };

  const handleDeleteDevice = (deviceId) => {
    setDevices(devices.filter(device => device.id !== deviceId));
  };

  const handleEditSelection = (deviceId) => {
    setSelectedDeviceId(deviceId);
    setEditMode(true);
  };

  return (
    <div>
      <h1>Device Management</h1>
      <AddDevice onAddDevice={handleAddDevice} />
      <DeviceList
        devices={devices}
        onDeleteDevice={handleDeleteDevice}
        onEditDevice={handleEditSelection}
        editMode={editMode}
        selectedDeviceId={selectedDeviceId}
        setEditMode={setEditMode}
      />
    </div>
  );
};

export default App;
