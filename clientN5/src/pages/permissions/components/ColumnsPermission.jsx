import { Space, Tag  } from "antd";
import moment from "moment";
import { CreateIcon, EditIcon } from "../../../common/components/svg/iconsSvg";
import { ADMIN, DEFAULT, USER } from "../../../utils/typesPermission";

export const ColumnsPermission = (requestPermission, openModalEdit) => {

const getColorForType = (type) => {
  switch (type) {
    case DEFAULT:
      return "magenta"
    case USER:
      return "blue"
    case ADMIN:
      return "gold"
    default:
      return "";
  }
}

const columns = [
  {
    title : "CHALLENGE N5",
    children : [
      {
      title: "Nombre(s)",
      align: "center",
      dataIndex: "nombreEmpleado",
      key: "nombreEmpleado"
    },
    {
      title: "Apellido(s)",
      align: "center",
      dataIndex: "apellidoEmpleado",
      key: "apellidoEmpleado",
    },
    {
      title: "Tipo permiso",
      align: "center",
      key: "tipoPermiso",
      dataIndex: "tipoPermiso",
      render: (item) => (            
              <Tag color={getColorForType(item.toUpperCase())}>
                {item.toUpperCase() === DEFAULT ? "SIN PERMISOS" : item.toUpperCase()}
              </Tag>
            )
    },
    {
      title: "Fecha permiso",
      align: "center",
      dataIndex: "fechaPermiso",
      key: "fechaPermiso",
      render: (item) => moment(item).format("YYYY-MM-DD HH:mm:ss")
    },
    {
      title: "Opciones",
      align: "center",
      key: "action",
      render: (record) => (
        <Space size="middle">
          {record.tipoPermiso === DEFAULT            
            ? <CreateIcon style={{ cursor: "pointer" }} onClick={() => requestPermission(record)}/>
            :  <EditIcon style={{ cursor: "pointer" }} onClick={() => openModalEdit(record)}/>}
        </Space>
      ),
    },
    ]
  }    
];  
  return columns;
}


 