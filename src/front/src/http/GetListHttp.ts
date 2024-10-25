export const GetList = async (SelectedKey, pageSize, currentPage) => {
  let response;
  switch (SelectedKey) {
    case 1:
      response = await fetch(
        `https://localhost:7119/api/Doctor/getList?PageNumbers=${pageSize}&SortBy=id&Page=${currentPage}`, {
          method: 'GET',
          headers: {
            'Accept': '*/*'
          }
        }
      );
      break;
    case 2:
      response = await fetch(
        `https://localhost:7119/api/Patient/getList?PageNumbers=${pageSize}&SortBy=id&Page=${currentPage}`, {
          method: 'GET',
          headers: {
            'Accept': '*/*'
          }
        }
      );
      break;
    default:
      throw new Error("Invalid SelectedKey");
  }

  if (!response.ok) {
    throw new Error("Failed to fetch data");
  }

  const result = await response.json();
    
  return result.map((item) => ({ ...item, key: item.id }));
};
