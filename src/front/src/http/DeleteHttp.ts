export const DeleteHttp =  (formData , SelectedKey) => {
    console.log(JSON.stringify(formData))
    switch(SelectedKey){
        case 1:{
            fetch(`https://localhost:7119/api/Doctor/delete?Id=${formData}`, {
                method: 'DELETE',
                headers: {
                  'Accept': '*/*',
                  'Content-Type': 'application/json'
                }
              });
        break;
        }
        case 2:{
          fetch('https://localhost:7119/api/Patient/delete', {
            method: 'DELETE',
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

      return;
  };
  