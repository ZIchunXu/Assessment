using Assessment.Data;
using Assessment.Services.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SynicTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assessment.Services
{
    public class BlogService : BaseService<AssessmentDataContext, Blog, BlogFilters>, IBlogService
    {
        public BlogService(AssessmentDataContext context)
            : base(context, x => x.Blogs)
        {
        }

        public IQueryable<Blog> All(BlogFilters filters, params Func<IQueryable<Blog>, IIncludableQueryable<Blog, object>>[] includes)
        {
            IQueryable<Blog> res = base.Context.Blogs.Include(x => x.Author);
            if (filters != null)
            {
                if(!string.IsNullOrEmpty(filters.Title))
                {
                    res = res.Where(x => x.Title.Contains(filters.Title));
                }
                if(filters.AuthorID != null)
                {
                    res = res.Where(x => x.AuthorID == filters.AuthorID);
                }
                if (!string.IsNullOrEmpty(filters.Body))
                {
                    res = res.Where(x => x.Body.Contains(filters.Body));
                }
            }
            return res;
        }
        public override ServiceResult<Blog> CreateUpdate(Blog entity)
        {

            //it shows CS0266 or CS0229  which are Microsoft.EntityFrameworkCore.DbSet<Assessment.Data.Author> can't covert
            //to Microsoft.EntityFrameworkCore.DbSet<Assessment.Data.Author>
            //or some similar error(DbSet, List..., eventhough I thought those type are totally same). 
            //IEnumerable<Author> authors = base.Context.Authors.AsEnumerable();  <============== idk why that doesnt work. 
            // It seems that is because the EntityFrameWork Version Issue. 
            var result = new ServiceResult<Blog>();
            try
            {
                if (entity.Id == 0)
                {
                    entity.Created = DateTime.Now;
                    base.Context.Blogs.Add(entity);
                    result.Success = true;
                    result.Object = entity;
                }
                else
                {
                    var blog = Context.Blogs.SingleOrDefault(x => x.Id == entity.Id);
                    if (blog == null)
                    {
                        result.Success = false;
                        result.AddError("blog", "Blog not found");
                    }
                    blog.Title = entity.Title;
                    blog.AuthorID = entity.AuthorID;
                    blog.Body = entity.Body;
                    blog.Status = entity.Status;
                    result.Success = true;
                    result.Object = entity;
                }
                base.Context.SaveChanges();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.AddError("Error", ex.Message);
            }
            return result;
        }

        public override ServiceResult<Blog> DeleteById(int id)
        {
            var result = new ServiceResult<Blog>();
            try
            {
                var entity = base.Context.Blogs.Find(id);
                if (entity == null)
                {
                    result.Success = false;
                    result.AddError("ID", "Blog not found");
                }
                else
                {
                    base.Context.Blogs.Remove(entity);
                    base.Context.SaveChanges();
                    result.Success = true;
                    result.Object= entity;
                }
            }
            catch(Exception ex)
            {
                result.Success=false;
                result.AddError("Error", ex.Message);
            }
            
            return result;
        }

        public override IQueryable<Blog> Get(BlogFilters filters, params Func<IQueryable<Blog>, IIncludableQueryable<Blog, object>>[] includes)
        {
            //var res = this.All(filters, includes);

            

            IQueryable<Blog> res = base.Context.Blogs.Include(x => x.Author);
            return res;
        }

        public override ServiceResult<Blog> GetById(int id, params Func<IQueryable<Blog>, IIncludableQueryable<Blog, object>>[] includes)
        {
            var result = new ServiceResult<Blog>();
            try
            {
                IQueryable<Blog> res = base.Context.Blogs.Include(x => x.Author);
                var query = res.SingleOrDefault(x => x.Id == id);
                if (query == null)
                {
                    result.Success = false;
                    result.AddError("ID", "ID not found");
                }
                else
                {
                    result.Success = true;
                    result.Object = query;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.AddError("Error", ex.Message);
            }

            return result;
        }
    }

    public interface IBlogService
    {
        IQueryable<Blog> All(BlogFilters filters, params Func<IQueryable<Blog>, IIncludableQueryable<Blog, object>>[] includes);
        ServiceResult<Blog> CreateUpdate(Blog entity);
        ServiceResult<Blog> DeleteById(int id);
        IQueryable<Blog> Get(BlogFilters filters = null, params Func<IQueryable<Blog>, IIncludableQueryable<Blog, object>>[] includes);
        ServiceResult<Blog> GetById(int id, params Func<IQueryable<Blog>, IIncludableQueryable<Blog, object>>[] includes);
        ServiceResult<IPageOfList<Blog>> GetPage(BlogFilters filters, int? page = null, int? size = null, params Func<IQueryable<Blog>, IIncludableQueryable<Blog, object>>[] includes);
    }
}
