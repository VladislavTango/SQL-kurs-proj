import React, { useState , useEffect } from 'react';
import {UserOutlined, TeamOutlined, ReloadOutlined } from '@ant-design/icons';
import { Layout, Menu, theme, Button, message } from 'antd';
import './App.css';
import TableComponent from './Components/TableComponent/TableComponent.tsx';
import AddModal from './Components/Modal/AddModal.tsx';
import RedactModal from "./Components/Modal/RedactModal.tsx"
import { GetList } from "./http/GetListHttp.ts";
import {useSessionStorage}  from "./hook/useSessionStorage.ts"
import {DeleteHttp} from "./http/DeleteHttp.ts"

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
  const [msg , SetMsg] = useState('');
  const [SelectedRow , SetSelectedRow] = useState(null);
  const [CurrentPage] = useSessionStorage('CurrentPage', 1) 
  const [pageSize] = useSessionStorage('pageSize', 50)
  
  const [selectedKey, setSelectedKey] = useState(1);
  const [data, setData] = useState([]); 

  const {
    token: { colorBgContainer },
  } = theme.useToken();

  const handleMenuClick = (e) => {
    setSelectedKey(Number(e.key));
  };


  const fetchData = async () => {
    try {
      const result = await GetList(selectedKey, pageSize, CurrentPage);
      
      setData(result);
    } catch (error) {
      console.error("Error fetching data:", error);
    }
  };
  
  const handlechildmsg = (msg) =>{
    SetMsg(msg);
  };

  const HandlChildSelectedRow = (row) => {
    console.log("SelectedRow updated:", row);
    SetSelectedRow(row);
  }  

  useEffect(() => {
    fetchData();
  }, [pageSize, CurrentPage, selectedKey,msg]);

const DeleteRow =async ()=>{
  if(SelectedRow === "null"){
    message.error("Выберите поле для редактирования")
    return;
  }
  DeleteHttp((SelectedRow).id , selectedKey)
  fetchData();
}
const Refresh = () =>{
  fetchData();
}

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
          <AddModal fields={ObjToSwitch[selectedKey - 1]} SelectedKey={selectedKey} msg = {handlechildmsg}/>

          <Button className="custom-button" type="primary" size="large" onClick={DeleteRow}>
            Удалить запись
          </Button>

            <RedactModal fields={ObjToSwitch[selectedKey - 1]} SelectedKey={selectedKey} msg = {handlechildmsg} RowInf = {JSON.stringify(SelectedRow)}></RedactModal>

          <Button className="custom-button" type="primary" size="large" onClick={Refresh}>
            <ReloadOutlined />
          </Button>
        </Header>
        <Content>
          <div className="table-container">
            <TableComponent fields={ObjToSwitch[selectedKey-1]}  data ={data} ParentSelectedRow = {HandlChildSelectedRow}/>
          </div>
        </Content>
      </Layout>
    </Layout>
  );
};

export default App;