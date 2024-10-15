import React from 'react';
import axios from 'axios';

const DeleteDevice = ({ deviceId, onDelete }) => {
  const handleDelete = () => {
    axios
      .delete(`http://localhost:5220/api/device/${deviceId}`)
      .then((response) => {
        alert(`Device with ID '${deviceId}' deleted successfully!`);
        onDelete(deviceId);  // Notify parent component about deletion
      })
      .catch((error) => {
        console.error('There was an error deleting the device!', error);
      });
  };

  return (
    <button onClick={handleDelete}>Delete Device</button>
  );
};

export default DeleteDevice;
