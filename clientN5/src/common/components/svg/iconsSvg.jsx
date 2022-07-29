import Icon from "@ant-design/icons";
import { createSvg, editSvg } from "./svgs";

export const EditIcon = (props) => (
  <Icon component={() => editSvg("2.2em", "2.2em")} {...props} />
);

export const CreateIcon = (props) => (
  <Icon component={() => createSvg("2.2em", "2.2em")} {...props} />
);