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
      title: "Nombre",
      dataIndex: "nombreEmpleado",
      key: "nombreEmpleado"
    },
    {
      title: "Apellido",
      dataIndex: "apellidoEmpleado",
      key: "apellidoEmpleado",
    },
    {
      title: "Fecha permiso",
      dataIndex: "fechaPermiso",
      key: "fechaPermiso",
      render: (item) => moment(item).format("YYYY-MM-DD HH:mm:ss")
    },
    {
      title: "Tipo permiso",
      key: "tipoPermiso",
      dataIndex: "tipoPermiso",
      render: (item) => (            
              <Tag color={getColorForType(item.toUpperCase())}>
                {item.toUpperCase() === DEFAULT ? "SIN PERMISOS" : item.toUpperCase()}
              </Tag>
            )
    },
    {
      title: "Opciones",      
      key: "action",
      render: (record) => (
        <Space size="middle">
          {record.tipoPermiso === DEFAULT            
            ? <CreateIcon style={{ cursor: "pointer" }} onClick={() => requestPermission(record)}/>
            :  <EditIcon style={{ cursor: "pointer" }} onClick={() => openModalEdit(record)}/>}
        </Space>
      ),
    },
];  
  return columns;
}


 