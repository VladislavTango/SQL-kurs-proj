export const AddHttp = async (formData , SelectedKey) => {
    let response;
    switch(SelectedKey){
        case 1:{
        response = await fetch('https://localhost:7119/api/Doctor/add', {
          method: 'POST',
          headers: {
            'Accept': '*/*',
            'Content-Type': 'application/json'
          },
          body: JSON.stringify(formData)
        })
        break;
        }
        case 2:{
          response = await fetch('https://localhost:7119/api/Patient/add', {
            method: 'POST',
            headers: {
              'Accept': '*/*',
              'Content-Type': 'application/json'
            },
            body: JSON.stringify(formData)
          })
        break;
        }
        default:console.log("):");
      };

      return await response.json();
  };
  