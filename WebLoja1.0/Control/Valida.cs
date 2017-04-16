using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLoja1._0.Model;

namespace WebLoja1._0.Control
{
    public class Valida
    {
        Repository dbRepository = new Repository();
        Usuarios user = new Usuarios();

        public bool ValidaUser(int id, int perfil)
        {
            user = dbRepository.pesquisaUserById(id);

            if (user.num_perfil == perfil)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}