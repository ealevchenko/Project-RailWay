using EFISA95.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFISA95.Backup
{
    [Serializable()]
    public class ChangeID
    {
        public int? ID
        {
            get;
            set;
        }
        public int? IDParent
        {
            get;
            set;
        }
    }

    [Serializable()]
    public class ResultID
    {
        public int ID
        {
            get;
            set;
        }
        public int? IDNew
        {
            get;
            set;
        }
    }

    /// <summary>
    /// Класс данных о результате выполнения одной строки скрипта
    /// </summary>
    [Serializable()]
    public class ResultIDScript : ResultID
    {
        public ResultIDScript() : base() { }
        public StringBuilder Script
        {
            get;
            set;
        }
    }
    /// <summary>
    /// Класс данных о результате выполнения всего масива скрипта
    /// </summary>
    [Serializable()]
    public class ResultScript
    {
        public List<ResultIDScript> ListResultID
        {
            get;
            set;
        }
        public StringBuilder ScriptOperation
        {
            get;
            set;
        }
        public StringBuilder ScriptDeclare
        {
            get;
            set;
        }
        public ResultScript()
        {
            this.ListResultID = new List<ResultIDScript>();
            this.ScriptOperation = new StringBuilder();
            this.ScriptDeclare = new StringBuilder();
        }

        public void Clear()
        {
            this.ListResultID.Clear();
            this.ScriptOperation.Clear();
        }
    }

    public class Backup
    {
        protected string path = @"D:\Backup_isa95\";
        public string Path { get { return this.path; } set { this.path = value; } }
        protected string owner = "Левченко Эдуард";
        public string Owner { get { return this.owner; } set { this.owner = value; } }

        public Backup()
        {

        }
        /// <summary>
        /// Определить коректное название файла
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetCorrectNameFile(string name)
        {
            return DateTime.Now.ToString("dd_MM_yyyy_") + name;
        }
        /// <summary>
        /// Определить заголовок
        /// </summary>
        /// <returns></returns>
        public StringBuilder TitleScript()
        {
            // Открываем для записи файл и заносим информацию
            // Заглавие файла
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("use [KRR-PA-ISA95_RailWay]");
            sb.AppendLine("/**************************************************");
            sb.AppendLine(" Файл создан автоматически");
            sb.AppendLine(" Дата и время создания файла :" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"));
            sb.AppendLine(" Владелец файла " + this.owner);
            sb.AppendLine("**************************************************/");
            return sb;
        }
        /// <summary>
        /// Сохранить скрипт в указаном файле
        /// </summary>
        /// <param name="rs"></param>
        /// <param name="namefile"></param>
        public void SaveScript(StringBuilder sb, string namefile)
        {
            if (!Directory.Exists(this.path))
            {
                Directory.CreateDirectory(this.path);
            }
            string PatchFile = this.path + GetCorrectNameFile(namefile) + ".sql";
            FileStream fsScr = new FileStream(@PatchFile, FileMode.Create);
            StreamWriter sw = new StreamWriter(fsScr);
            sw.Write(TitleScript());
            sw.Write(sb);
            sw.Flush();
            sw.Close();
        }
        /// <summary>
        /// Сохранить скрипт в указаном файле
        /// </summary>
        /// <param name="rs"></param>
        /// <param name="namefile"></param>
        public void SaveScript(ResultScript rs, string namefile)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(rs.ScriptDeclare);
            sb.Append(rs.ScriptOperation);
            SaveScript(sb, namefile);
        }

    }

    public class BackupTable<T> : Backup
    {
        private ResultScript resultscript;
        public ResultScript ResultScript { get { return this.resultscript; } set { this.resultscript = value; } }
        private string namefile = "ins_upd_" + typeof(T).ToString();
        public string NameFile { get { return this.namefile; } set { this.namefile = value; } }
        protected ScriptTable ScriptTable;

        public BackupTable()
            : base()
        {
            this.resultscript = new ResultScript();
            DeclareTable();
        }

        #region Вспомогательные функции
        /// <summary>
        /// Проверить наличие id в списке
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idlist"></param>
        /// <returns></returns>
        protected bool IsListID(int id, int[] idlist)
        {
            if (idlist == null) return false;
            foreach (int idstop in idlist)
            {
                if (id == idstop) return true;
            }
            return false;
        }
        /// <summary>
        /// Вернуть новый id по старому id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lr"></param>
        /// <returns></returns>
        protected int? GetNewID(int? id, List<ResultIDScript> lr)
        {
            if (lr == null | id == null) return null;
            foreach (ResultIDScript rid in lr)
            {
                if (rid.ID == id) return rid.IDNew;
            }
            return null;
        }
        #endregion

        #region Формирование скрипта
        /// <summary>
        /// Определить таблицу
        /// </summary>
        public virtual void DeclareTable()
        {
            this.ScriptTable = null;
        }
        /// <summary>
        /// Присвоить переменные скрипту
        /// </summary>
        /// <param name="type_data"></param>
        /// <param name="cid"></param>
        /// <returns></returns>
        public virtual StringBuilder SetTable(T type_data, ChangeID cid)
        {
            StringBuilder swriter = new StringBuilder();

            return swriter;
        }
        /// <summary>
        /// Определение заголовка скрипта и опесание переменных
        /// </summary>
        /// <returns></returns>
        public virtual StringBuilder Declare()
        {
            StringBuilder swriter = new StringBuilder();
            if (this.ScriptTable != null)
            {
                Scripts scr = new Scripts();
                swriter.AppendLine("/*********************************************");
                swriter.AppendLine(String.Format(" Заполнение таблицы {0} ", this.ScriptTable.NameTable));
                swriter.AppendLine("*********************************************/");
                swriter.AppendLine("--> Объявим переменные");
                foreach (ScriptField sf in this.ScriptTable.ScriptField)
                {
                    swriter.AppendLine(String.Format("declare @{0} {1};", sf.NameField, scr.GetSQLType(sf.FieldType, sf.FieldSize)));
                }
            }
            return swriter;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual StringBuilder DeclareInsert()
        {
            StringBuilder sb = new StringBuilder();

            if (this.ScriptTable != null)
            {
                Scripts scr = new Scripts();
                StringBuilder sbd = new StringBuilder();
                StringBuilder sbv = new StringBuilder();
                sb.AppendLine(String.Format("   INSERT INTO [{0}].[{1}] ", this.ScriptTable.Scheme, this.ScriptTable.NameTable));
                bool comma = false;
                foreach (ScriptField sf in this.ScriptTable.ScriptField)
                {
                    sbd.Append(String.Format("{0}[{1}]", comma ? "," : "       (", sf.NameField));
                    sbv.Append(String.Format("{0}@{1}", comma ? "," : "        VALUES(", sf.NameField));
                    comma = true;
                }
                sb.AppendLine(sbd + ")");
                sb.AppendLine(sbv + ")");
            }
            return sb;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual StringBuilder DeclareUpdate()
        {
            StringBuilder sb = new StringBuilder();

            if (this.ScriptTable != null)
            {
                Scripts scr = new Scripts();
                StringBuilder sbwhere = new StringBuilder();
                sb.AppendLine(String.Format("   UPDATE [{0}].[{1}]", this.ScriptTable.Scheme, this.ScriptTable.NameTable));
                bool comma = false;
                foreach (ScriptField sf in this.ScriptTable.ScriptField)
                {
                    if (!sf.FieldKey)
                    {
                        sb.AppendLine(String.Format("      {0}[{1}] = @{1}", comma ? "," : "SET", sf.NameField));
                        comma = true;
                    }
                    else
                    {
                        sbwhere.AppendLine(String.Format("       WHERE [{0}] = @{0}", sf.NameField));
                    }
                }
                sb.Append(sbwhere);
            }
            return sb;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual StringBuilder DeclareIU()
        {
            StringBuilder sb = new StringBuilder();

            if (this.ScriptTable != null)
            {
                Scripts scr = new Scripts();
                sb.AppendLine(String.Format("if (not exists(SELECT [{0}] FROM [{1}].[{2}] WHERE ([{0}] = @{0})))", scr.GetNameKey(this.ScriptTable.ScriptField), this.ScriptTable.Scheme, this.ScriptTable.NameTable));
                sb.AppendLine(" BEGIN");
                sb.AppendLine(" --> Строка отсутствует, создадим новую строку");
                sb.Append(DeclareInsert());
                sb.AppendLine(" END ELSE BEGIN");
                sb.AppendLine(" --> Строка с указанным ID существует, обновим строку");
                sb.Append(DeclareUpdate());
                sb.AppendLine(" END");
            }
            return sb;
        }
        /// <summary>
        /// Определение тела скрипта (вставка и обновление)
        /// </summary>
        /// <param name="ec"></param>
        /// <returns></returns>
        public virtual StringBuilder IUScript(T type_data, ref List<ResultIDScript> lr, ref  int? idrenumbering)
        {
            StringBuilder sb = new StringBuilder();
            int? parentidrenumbering = GetNewID(((IDescription)type_data).ParentID, lr); // определим новый id родителя
            //idrenumbering, parentidrenumbering
            sb.Append(SetTable(type_data, new ChangeID() { ID = idrenumbering, IDParent = parentidrenumbering }));
            sb.AppendLine(String.Format("--> Проверим наличие строки {0} с ID {1}", ((IDescription)type_data).Description, ((IDescription)type_data).ID));
            sb.Append(DeclareIU());
            lr.Add(new ResultIDScript() { Script = sb, ID = ((IDescription)type_data).ID, IDNew = (idrenumbering == null) ? null : idrenumbering++ });
            return sb;

        }
        /// <summary>
        /// Вернуть потомков
        /// </summary>
        /// <param name="type_data"></param>
        /// <returns></returns>
        public virtual IQueryable<T> GetChild(T type_data)
        {
            return null;
        }
        /// <summary>
        /// вернуть родителя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual IQueryable<T> GetRoot(int id)
        {
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentid"></param>
        /// <returns></returns>
        public virtual IQueryable<T> GetRootParentID(int? parentid)
        {
            return null;
        }
        /// <summary>
        /// вернуть все строки владельца
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual IQueryable<T> GetRootOwner(int? idOwner)
        {
            return null;
        }

        /// <summary>
        /// Добавить скрипт потомков
        /// </summary>
        /// <param name="type_data"></param>
        /// <param name="swriter"></param>
        private void ChildScript(T type_data, ref StringBuilder swriter, ref List<ResultIDScript> lr, int[] idStop, ref int? idrenumbering)
        {
            IQueryable<T> custs = GetChild(type_data);
            foreach (T cust in custs)
            {
                swriter.AppendLine("/*********************************************");
                swriter.AppendLine(String.Format(" Заполнение Root: {0}  Child: {1}", ((IDescription)type_data).Description, ((IDescription)cust).Description));
                swriter.AppendLine("*********************************************/");
                swriter.Append(IUScript(cust, ref lr, ref idrenumbering));
                if (!IsListID(((IDescription)cust).ID, idStop))
                {
                    ChildScript(cust, ref swriter, ref lr, idStop, ref idrenumbering);
                }
            }
        }
        #endregion

        #region Сформировать скрипт по ID
        /// <summary>
        /// Добавить скрипт родителя
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        public StringBuilder RootScriptInID(int start, ref List<ResultIDScript> lr, int[] idStop, ref int? idrenumbering)
        {
            StringBuilder swriter = new StringBuilder();
            //swriter.Append(Declare());

            IQueryable<T> custs = GetRoot(start);
            try
            {
                foreach (T cust in custs)
                {
                    swriter.Append(IUScript(cust, ref lr, ref idrenumbering));
                    if (!IsListID(((IDescription)cust).ID, idStop))
                    {
                        ChildScript(cust, ref swriter, ref lr, idStop, ref idrenumbering);
                    }

                }
            }
            catch (Exception e)
            {

            }
            return swriter;
        }
        /// <summary>
        ///  Создать скрипт родителя и потомков результат вернуть ResultScript
        /// </summary>
        /// <param name="idRoot">ID родителя</param>
        /// <param name="idStop">ID последних включаемых потомков</param>
        /// <param name="idrenumbering">старт ID перенумерации</param>
        /// <returns></returns>
        public ResultScript CreateIUScriptInID(int idRoot, int[] idStop, int? idrenumbering)
        {
            ResultScript rs = new ResultScript();
            List<ResultIDScript> lrid = rs.ListResultID;
            rs.ScriptDeclare = Declare();
            rs.ScriptOperation.Append(RootScriptInID(idRoot, ref lrid, idStop, ref idrenumbering));
            return rs;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idRoot"></param>
        /// <returns></returns>
        public ResultScript CreateIUScriptInID(int idRoot)
        {
            return CreateIUScriptInID(idRoot, null, null);
        }
        /// <summary>
        /// Создать скрипт родителя и потомков результат сохране в классе
        /// </summary>
        /// <param name="idRoot">ID родителя</param>
        /// <param name="idStop">ID последних включаемых потомков</param>
        /// <param name="idrenumbering">старт ID перенумерации</param>
        public void GetIUScriptInID(int idRoot, int[] idStop, int? idrenumbering)
        {
            this.resultscript.Clear();
            this.resultscript = CreateIUScriptInID(idRoot, idStop, idrenumbering);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idRoot"></param>
        public void GetIUScriptInID(int idRoot)
        {
            GetIUScriptInID(idRoot, null, null);
        }
        #endregion

        #region  Сформировать скрипт по ParentID
        /// <summary>
        /// Получить родителя
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        public StringBuilder RootScriptParentID(int? Parentid, ref List<ResultIDScript> lr, int[] idStop, ref int? idrenumbering)
        {
            StringBuilder swriter = new StringBuilder();
            //swriter.Append(Declare());

            IQueryable<T> custs = GetRootParentID(Parentid);
            try
            {
                foreach (T cust in custs)
                {
                    if (!IsListID(((IDescription)cust).ID, idStop))
                    {
                        swriter.Append(IUScript(cust, ref lr, ref idrenumbering));
                        ChildScript(cust, ref swriter, ref lr, idStop, ref idrenumbering);
                    }

                }
            }
            catch (Exception e)
            {

            }
            return swriter;
        }
        /// <summary>
        /// Создать скрипт потомков результат вернуть ResultScript
        /// </summary>
        /// <param name="Parentid"></param>
        /// <param name="idStop"></param>
        /// <param name="idrenumbering"></param>
        /// <returns></returns>
        public ResultScript CreateIUScriptInParentID(int? Parentid, int[] idStop, int? idrenumbering)
        {
            ResultScript rs = new ResultScript();
            List<ResultIDScript> lrid = rs.ListResultID;
            rs.ScriptDeclare = Declare();
            rs.ScriptOperation.Append(RootScriptParentID(Parentid, ref lrid, idStop, ref idrenumbering));
            return rs;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Parentid"></param>
        /// <returns></returns>
        public ResultScript CreateIUScriptInParentID(int? Parentid)
        {
            return CreateIUScriptInParentID(Parentid, null, null);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Parentid"></param>
        /// <param name="idStop"></param>
        /// <param name="idrenumbering"></param>
        public void GetIUScriptInParentID(int? Parentid, int[] idStop, int? idrenumbering)
        {
            this.resultscript.Clear();
            this.resultscript = CreateIUScriptInParentID(Parentid, idStop, idrenumbering);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idRoot"></param>
        public void GetIUScriptInParentID(int? Parentid)
        {
            GetIUScriptInParentID(Parentid, null, null);
        }
        #endregion

        #region  Сформировать скрипт по Owner
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Parentid"></param>
        /// <param name="lr"></param>
        /// <param name="idStop"></param>
        /// <param name="idrenumbering"></param>
        /// <returns></returns>
        public StringBuilder RootScriptOwner(int owner, ref List<ResultIDScript> lr, int[] idStop, ref int? idrenumbering)
        {
            StringBuilder swriter = new StringBuilder();
            //swriter.Append(Declare());

            IQueryable<T> custs = GetRootOwner(owner);
            try
            {
                foreach (T cust in custs)
                {
                    swriter.Append(IUScript(cust, ref lr, ref idrenumbering));
                    if (!IsListID(((IDescription)cust).ID, idStop))
                    {
                        ChildScript(cust, ref swriter, ref lr, idStop, ref idrenumbering);
                    }

                }
            }
            catch (Exception e)
            {

            }
            return swriter;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="idStop"></param>
        /// <param name="idrenumbering"></param>
        /// <returns></returns>
        public ResultScript CreateIUScriptInOwner(int owner, int[] idStop, int? idrenumbering)
        {
            ResultScript rs = new ResultScript();
            List<ResultIDScript> lrid = rs.ListResultID;
            rs.ScriptDeclare = Declare();
            rs.ScriptOperation.Append(RootScriptOwner(owner, ref lrid, idStop, ref idrenumbering));
            return rs;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        public ResultScript CreateIUScriptInOwner(int owner)
        {
            return CreateIUScriptInOwner(owner, null, null);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Parentid"></param>
        /// <param name="idStop"></param>
        /// <param name="idrenumbering"></param>
        public void GetIUScriptInOwner(int owner, int[] idStop, int? idrenumbering)
        {
            this.resultscript.Clear();
            this.resultscript = CreateIUScriptInOwner(owner, idStop, idrenumbering);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idRoot"></param>
        public void GetIUScriptInOwner(int owner)
        {
            GetIUScriptInOwner(owner, null, null);
        }

        #endregion

        #region Сохранение скриптов в файл
        /// <summary>
        /// Сохранить скрипт класса
        /// </summary>
        /// <param name="namefile"></param>
        public void SaveIUScript(string namefile)
        {
            base.SaveScript(this.resultscript, namefile);
        }
        /// <summary>
        /// Сохранить скрипт класса
        /// </summary>
        public void SaveIUScript()
        {
            SaveIUScript(this.namefile);
        }
        #endregion
    }

}
