import React, { useEffect, useState } from 'react';
import type { GetProp, TableProps } from 'antd';
import { Table } from 'antd'; 
import type { SorterResult } from 'antd/es/table/interface';
import './TableComponent.css'; 
import { useSessionStorage } from 'usehooks-ts';

type TablePaginationConfig = Exclude<GetProp<TableProps, 'pagination'>, boolean>;

interface TableParams {
  pagination?: TablePaginationConfig;
  sortField?: SorterResult<any>['field'];
  filters?: Parameters<GetProp<TableProps, 'onChange'>>[1];
}

let clickedRowElement: HTMLElement | null = null;

const TableComponent = ({fields,data,ParentSelectedRow}) => {
  
  const [tableParams, setTableParams] = useState<TableParams>({
    pagination: {
      current: 1,
      pageSize: 50,
    },
  });
  
  const [, setvalueCurrent] = useSessionStorage("CurrentPage",tableParams.pagination?.current);
  const [, setvaluePage ] = useSessionStorage("pageSize",tableParams.pagination?.pageSize);

  const StorageSet = (tableParams) => {
    setvalueCurrent(tableParams.pagination?.current);
    setvaluePage(tableParams.pagination?.pageSize)
    setTableParams({ 
      ...tableParams, 
      pagination: { 
        ...tableParams.pagination, 
        total: 200,
      }, 
    });
  }

  useEffect(() => {
    StorageSet(tableParams);
  }, [
    tableParams.pagination?.current,
    tableParams.pagination?.pageSize,
    tableParams?.sortOrder,
    tableParams?.sortField,
    JSON.stringify(tableParams.filters),
  ]);

  const handleTableChange = (pagination, filters) => {
    setTableParams({
      pagination,
      filters
    });
  };

  const handleRowClick = (record, event) => {
    
    ParentSelectedRow(record); 

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
    if(typeof(fieldValue) === 'string'&&fieldValue.length>15){
      fieldValue = fieldValue.substring(0,15);
      fieldValue+="...";
    }
    // DIRECT BY MIROSLAV LAPANIK
    // if (typeof(String(fieldValue)) === "string") {
    //   if (String(fieldValue).length >= 10) {
    //     if (String(fieldValue) !== "Мирослав"){
    //       let arr = String(fieldValue).split('')
    //       let str: Array<String> = []
    //       let count = 0
    //       while (true) {
    //         str.push(arr[count])
    //         count += 1
    //         if (count === 15){
    //           break
    //         }
    //       }
    //       count = 0
    //       while (true) {
    //         str.push(".")
    //         count ++
    //         if (count === 3){
    //           break
    //         }
    //       }
    //       let qwe = str.join('')
    //       fieldValue = qwe;

    //     }
    //   }
    // }

    return fieldValue;
  };

  const columns = [
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
      render: (text: any, record) => fieldNameChecker(field.name, record[field.name]),
    }))
  ];

  return (
    <div className="table-container">
      <Table
        columns={columns}
        dataSource={data}
        pagination={tableParams.pagination}
        onRow={(record) => ({
          onClick: (event) => handleRowClick(record, event),
        })}
        onChange={handleTableChange}
      />
    </div>
  );
};

export default TableComponent;