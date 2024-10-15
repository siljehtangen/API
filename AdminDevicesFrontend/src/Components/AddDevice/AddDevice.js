import React, { useState } from 'react';
import axios from 'axios';

const AddDevice = ({ onAddDevice }) => {
  const [device, setDevice] = useState({ name: '', status: '', type: '' });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setDevice({ ...device, [name]: value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    axios.post('http://localhost:5220/api/device', device)
    .then((response) => {
      console.log('Response:', response); // Check the response status
      if (response.status === 201 || response.status == 200) {
        alert('Device added successfully!');
        onAddDevice(response.data); // Update parent state with the new device
      } else {
        alert('Unexpected response from the server.');
      }
    })
    .catch((error) => {
      console.error('There was an error adding the device!', error);
      alert(`Error: ${error.message}`);
    });
  
  };

  return (
    <form onSubmit={handleSubmit}>
      <h2>Add a New Device</h2>
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
      <button type="submit">Add Device</button>
    </form>
  );
};

export default AddDevice;
