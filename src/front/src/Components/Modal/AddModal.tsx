import React, { useState } from 'react';
import { Button, Modal, Input, Space, message,Select } from 'antd';
import { AddHttp } from '../../http/AddHttp.ts';

const AddModal = ({ fields, SelectedKey ,msg}) => {  
  const [formData, setFormData] = useState({});
  const [isModalOpen, setIsModalOpen] = useState(false);

  const validateFields = () => {
    for (const field of fields) {
      if (!formData[field.name]) {
        message.error(`Поле "${field.placeholder}" должно быть заполнено`);
        return false;
      }
      if (field.type === 'number' && !/^\d+$/.test(formData[field.name])) {
        message.error(`Поле "${field.placeholder}" должно содержать только цифры`);
        return false;
      }
    }
    
    return true;
  };

  const onOk = () => {
    if (validateFields()) {
      handleOk();
    }
  };


  const showModal = () => {
    setIsModalOpen(true);
  };

  const handleOk = async () => {
    let response = AddHttp(formData , SelectedKey);

    msg(response);

    setIsModalOpen(false);
  };

  const handleCancel = () => {
    setIsModalOpen(false);
  };

  const handleChange = (e, name, type) => {
    setFormData({ ...formData, [name]:type ==='number'? Number(e.target.value) : String(e.target.value) });
    
  };


  const random = (field) => {
    if (field.type !== "select") 
      {
      return (
        <Input
              key={field.name}
              className='valid'
              status=''
              size="large"
              placeholder={field.placeholder}
              value={formData[field.name] || ''}
              onChange={(e) => handleChange(e, field.name , field.type)}
              type={field.type}
            />
      )
    } 
    else 
    {
      return (
        <Select placeholder="Пол" style={{width:"100%"}}>
          <option value={formData[field.name] = 0}>мужской</option>
          <option value={formData[field.name] = 1}>женский</option>
        </Select>
      ) 
    }
  }
 
  return (
    <div>
      <Button size='large' type="primary" htmlType='submit' onClick={showModal}>
        Добавить запись
      </Button>
      <Modal title="Добавление записи в базу" open={isModalOpen} onOk={onOk} onCancel={handleCancel}>
        <Space direction="vertical" style={{ width: '100%' }}>
          {fields.map((field) => random(field))}
        </Space>
      </Modal>
    </div>
  );
};

export default AddModal;
