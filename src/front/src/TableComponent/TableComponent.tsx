import React, { useEffect, useState } from 'react';
import type { GetProp, TableProps } from 'antd';
import { Table } from 'antd';
import type { SorterResult } from 'antd/es/table/interface';
import './TableComponent.css'; 

type ColumnsType<T extends object = object> = TableProps<T>['columns'];
type TablePaginationConfig = Exclude<GetProp<TableProps, 'pagination'>, boolean>;

interface DataType {
  idid: number;
  fio: string;
  roomNumber: number;
  specializationName: string;
  region: number;
}

interface TableParams {
  pagination?: TablePaginationConfig;
  sortField?: SorterResult<any>['field'];
  sortOrder?: SorterResult<any>['order'];
  filters?: Parameters<GetProp<TableProps, 'onChange'>>[1];
}

const columns: ColumnsType<DataType> = [
  {
    title: 'Id',
    dataIndex: 'id',
    width: '5%',
  },
  {
    title: 'FIO',
    dataIndex: 'fio',
    width: '30%',
  },
  {
    title : "specialisation",
     dataIndex: 'specializationName',
    width: '30%',
  },
  {
    title: 'Room',
    dataIndex: 'roomNumber',
  },
  {
    title:"Region",
    dataIndex : "region"
  }
];

const TableComponent = () => {
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
    try {
      const response = await fetch(`https://localhost:7119/api/Doctor/getList?SortBy=id&Page=${tableParams.pagination?.current}`, {
        method: 'GET',
        headers: {
          'Accept': '*/*'
        }
      });

      if (!response.ok) {
        throw new Error('Ошибка сети или сервера');
      }

      const result = await response.json();
      setData(result); 
      console.log(result);
      
      setLoading(false);
      
      setTableParams({
        ...tableParams,
        pagination: {
          ...tableParams.pagination,
          total: 200, 
        },
      });
    } catch (error) {
      console.error('Ошибка:', error);
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchData(); // Вызываем fetchData при изменении параметров пагинации, сортировки и фильтрации
  }, [
    tableParams.pagination?.current,
    tableParams.pagination?.pageSize,
    tableParams?.sortOrder,
    tableParams?.sortField,
    JSON.stringify(tableParams.filters),
  ]);

  const handleTableChange: TableProps<DataType>['onChange'] = (pagination, filters, sorter) => {
    setTableParams({
      pagination,
      filters,
      sortOrder: Array.isArray(sorter) ? undefined : sorter.order,
      sortField: Array.isArray(sorter) ? undefined : sorter.field,
    });

    if (pagination.pageSize !== tableParams.pagination?.pageSize) {
      setData([]);
    }
  };

  return (
    <div className="table-container">
    <Table<DataType>
  columns={columns}
  dataSource={data}
  pagination={tableParams.pagination}
  loading={loading}
  onChange={handleTableChange}
/>


    </div>
  );
};

export default TableComponent;
