import React, { useState } from 'react';
import { IdcardOutlined, UserOutlined, FieldNumberOutlined, EnvironmentFilled, TeamOutlined, ReloadOutlined } from '@ant-design/icons';
import { Layout, Menu, theme, Button } from 'antd';
import './App.css';
import TableComponent from './TableComponent/TableComponent.tsx';
import AddModal from './Modal/AddModal.tsx';

const Doctorfields = [
  { name: 'fio', placeholder: 'ФИО', type: 'text' },
  { name: 'specialization', placeholder: 'Специализация', type: 'text' },
  { name: 'roomNumber', placeholder: 'Кабинет', type: 'number' },
  { name: 'region', placeholder: 'Регион', type: 'number' },
];
const PatientField = [
  { name: 'surname', placeholder: 'Фамилия', type: 'text' },
  { name: 'name', placeholder: 'Имя', type: 'text' },
  { name: 'patronymic', placeholder: 'Отчество', type: 'text' },
  { name: 'address', placeholder: 'Адресс', type: 'text' },
  { name: 'bornTime', placeholder: 'Дата рождения', type: 'date' },
  { name: 'sex', placeholder: 'Пол', type: 'select' },
  { name: 'patientRegion', placeholder: 'Регион', type: 'number' },
];
const ObjToSwitch = [Doctorfields, PatientField];

const { Header, Content, Sider } = Layout;

const NavNames = ["Доктора", "Пациенты"];

const items = [UserOutlined, TeamOutlined].map(
  (icon, index) => ({
    key: String(index + 1),
    icon: React.createElement(icon),
    label: NavNames[index],
  }),
);


const App  = () => {
  const [selectedKey, setSelectedKey] = useState(1);

  const {
    token: { colorBgContainer, borderRadiusLG },
  } = theme.useToken();

  const handleMenuClick = (e) => {
    console.log((e.key));
    
    setSelectedKey(Number(e.key));
  };

  return (
    <Layout className="full-height-layout">
      <Sider
        breakpoint="lg"
        collapsedWidth="0"
        onBreakpoint={(broken) => {
          console.log(broken);
        }}
        onCollapse={(collapsed, type) => {
          console.log(collapsed, type);
        }}
      >
        <div className="demo-logo-vertical" />
        <Menu theme="dark" mode="inline" defaultSelectedKeys={[String(selectedKey)]} items={items} onClick={handleMenuClick}/>
      </Sider>
      <Layout>
        <Header style={{ padding: 0, background: colorBgContainer }}>
          <AddModal fields={ObjToSwitch[selectedKey - 1]} SelectedKey={selectedKey} />

          <Button className="custom-button" type="primary" size="large">
            Удалить запись
          </Button>

          <Button className="custom-button" type="primary" size="large">
            Редактировать запись
          </Button>

          <Button className="custom-button" type="primary" size="large">
            <ReloadOutlined />
          </Button>
        </Header>
        <Content>
          <div className="table-container">
            <TableComponent fields={ObjToSwitch[selectedKey-1]}  SelectedKey = {selectedKey}/>
          </div>
        </Content>
      </Layout>
    </Layout>
  );
};

export default App;
