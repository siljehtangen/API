import React, { useState, useEffect } from 'react';
import axios from 'axios';

const EditDevice = ({ deviceId }) => {
  const [device, setDevice] = useState({ name: '', status: '', type: '' });

  useEffect(() => {
    // Fetch the existing device data based on deviceId
    axios.get(`http://localhost:5220/api/devices/${deviceId}`)
      .then((response) => {
        setDevice(response.data);
      })
      .catch((error) => {
        console.error('There was an error fetching the device details!', error);
      });
  }, [deviceId]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setDevice({ ...device, [name]: value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    axios.put(`http://localhost:5220/api/device/${deviceId}`, device)
      .then((response) => alert('Device updated successfully!'))
      .catch((error) => console.error('There was an error updating the device!', error));
  };

  return (
    <form onSubmit={handleSubmit}>
      <h2>Edit Device</h2>
      <label>
        Name:
        <input type="text" name="name" value={device.name} onChange={handleChange} required />
      </label>
      <label>
        Status:
        <input type="text" name="status" value={device.status} onChange={handleChange} required />
      </label>
      <label>
        Type:
        <input type="text" name="type" value={device.type} onChange={handleChange} required />
      </label>
      <button type="submit">Update Device</button>
    </form>
  );
};

export default EditDevice;
