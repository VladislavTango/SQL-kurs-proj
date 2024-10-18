import React, { useEffect, useState } from 'react';
import type { GetProp, TableProps } from 'antd';
import { Table } from 'antd'; 
import type { SorterResult } from 'antd/es/table/interface';
import './TableComponent.css'; 
import { GetList } from "../http/GetList"

type ColumnsType<T extends object = object> = TableProps<T>['columns'];
type TablePaginationConfig = Exclude<GetProp<TableProps, 'pagination'>, boolean>;

interface DataType {
  id: number;
  fio: string;
  roomNumber: number;
  specializationName: string;
  region: number;
}

interface Field {
  name: string;
  placeholder: string;
  type: string;
}

interface TableParams {
  pagination?: TablePaginationConfig;
  sortField?: SorterResult<any>['field'];
  sortOrder?: SorterResult<any>['order'];
  filters?: Parameters<GetProp<TableProps, 'onChange'>>[1];
}

interface TableComponentProps {
  fields: Field[];
  SelectedKey:number;
}

let SelectedRow = null;
let clickedRowElement: HTMLElement | null = null;

const TableComponent: React.FC<TableComponentProps> = ({fields,SelectedKey}) => {
  
  const [data, setData] = useState<DataType[]>([]);
  const [loading, setLoading] = useState(false);
  const [tableParams, setTableParams] = useState<TableParams>({
    pagination: {
      current: 1,
      pageSize: 50,
    },
  });

  const fetchData = async () => { 
    setLoading(true); 
    let response : Response; 
    switch(SelectedKey){ 
      case 1: { 
        response = await fetch( 
          `https://localhost:7119/api/Doctor/getList?PageNumbers= 
          ${tableParams.pagination?.pageSize}&SortBy=id&Page=${tableParams.pagination?.current}`, { 
          method: 'GET', 
          headers: { 
            'Accept': '*/*'
          } 
        }); 
        break; 
      } 
      case 2: { 
        response = await fetch( 
          `https://localhost:7119/api/Patient/getList?PageNumbers= 
          ${tableParams.pagination?.pageSize}&SortBy=id&Page=${tableParams.pagination?.current}`, { 
          method: 'GET', 
          headers: { 
            'Accept': '*/*' 
          } 
        }); 
        break; 
      } 
      default: { 
        break;
      } 
    } 
    let result = response != null ? await response.json() : console.log("респонс пустой"); 

    setData(result.map((item) => ({ 
      ...item, 
      key: item.id,
    })));

    setLoading(false);
    setTableParams({ 
      ...tableParams, 
      pagination: { 
        ...tableParams.pagination, 
        total: 200,
      }, 
    });
  }
  useEffect(() => {
    fetchData();
  }, [
    tableParams.pagination?.current,
    tableParams.pagination?.pageSize,
    tableParams?.sortOrder,
    tableParams?.sortField,
    JSON.stringify(tableParams.filters),
    SelectedKey
  ]);

  const handleTableChange: TableProps<DataType>['onChange'] = (pagination, filters, sorter) => {
    setTableParams({
      pagination,
      filters
    });

    if (pagination.pageSize !== tableParams.pagination?.pageSize) {
      setData([]);
    }
  };

  const handleRowClick = (record, event) => {
    SelectedRow = record; 

    if(clickedRowElement != null)
      clickedRowElement.classList.remove("selected");  

    clickedRowElement = event.currentTarget.closest('tr'); 
    clickedRowElement.classList.add("selected") 
  };
  const fieldNameChecker = (fieldName: string, fieldValue: any) => {
    if(fieldName === "bornTime"){
    fieldValue = String(fieldValue).split("T")[0].split("-").reverse().join("-");   
    }
    if (fieldName === "sex") {
      return fieldValue === 0 ? "мужской" : "женский";
    }
    return fieldValue;
  };
  const columns: ColumnsType<DataType> = [
    {
      title: 'Id',
      dataIndex: 'id',
      key: 'id', 
      width: '5%',
    },
    ...fields.map((field, index) => ({
      title: field.placeholder,
      dataIndex: field.name,
      key: `${field.name}-${index}`,
      width: "10%",
      render: (text: any, record: DataType) => fieldNameChecker(field.name, record[field.name]),
    }))
  ];

  return (
    <div className="table-container">
      <Table<DataType>
        columns={columns}
        dataSource={data}
        pagination={tableParams.pagination}
        loading={loading}
        onRow={(record) => ({
          onClick: (event) => handleRowClick(record, event),
        })}
        onChange={handleTableChange}
      />
    </div>
  );
};

export default TableComponent;
