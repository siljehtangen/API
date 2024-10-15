import React from 'react';
import DeleteDevice from '../DeleteDevice/DeleteDevice';
import EditDevice from '../EditDevice/EditDevice';

const DeviceList = ({ devices, onDeleteDevice, onEditDevice, editMode, selectedDeviceId, setEditMode }) => {
  return (
    <div>
      <h2>Device List</h2>
      <table border="1" cellPadding="5" cellSpacing="0">
        <thead>
          <tr>
            <th>ID</th>
            <th>Name</th>
            <th>Status</th>
            <th>Type</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {devices.map((device) => (
            <tr key={device.id}>
              <td>{device.id}</td>
              <td>{device.name}</td>
              <td>{device.status}</td>
              <td>{device.type}</td>
              <td>
                <button onClick={() => onEditDevice(device.id)} style={{ marginRight: '10px' }}>Edit</button>
                <DeleteDevice deviceId={device.id} onDelete={() => onDeleteDevice(device.id)} />
              </td>
            </tr>
          ))}
        </tbody>
      </table>

      {editMode && selectedDeviceId && (
        <EditDevice deviceId={selectedDeviceId} setEditMode={setEditMode} />
      )}
    </div>
  );
};

export default DeviceList;
